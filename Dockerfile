# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy appsettings.json from secret
ENV APPSETTINGS_JSON=""

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CFMS.Api/CFMS.Api.csproj", "src/CFMS.Api/"]
COPY ["src/CFMS.Application/CFMS.Application.csproj", "src/CFMS.Application/"]
COPY ["src/CFMS.Infrastructure/CFMS.Infrastructure.csproj", "src/CFMS.Infrastructure/"]
COPY ["src/CFMS.Domain/CFMS.Domain.csproj", "src/CFMS.Domain/"]
RUN dotnet restore "./src/CFMS.Api/CFMS.Api.csproj"
COPY . .
WORKDIR "/src/src/CFMS.Api"
RUN dotnet build "./CFMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CFMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CFMS.Api.dll"]
