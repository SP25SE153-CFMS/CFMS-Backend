FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["CFMS.Api/CFMS.Api.csproj", "CFMS.Api/"]
COPY ["CFMS.Application/CFMS.Application.csproj", "CFMS.Application/"]
COPY ["CFMS.Infrastructure/CFMS.Infrastructure.csproj", "CFMS.Infrastructure/"]
COPY ["CFMS.Domain/CFMS.Domain.csproj", "CFMS.Domain/"]

RUN dotnet restore "./CFMS.Api/CFMS.Api.csproj"

COPY . .

WORKDIR "/src/CFMS.Api"
RUN dotnet build "CFMS.Api.csproj" -c ${BUILD_CONFIGURATION:-Release} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CFMS.Api.csproj" -c ${BUILD_CONFIGURATION:-Release} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CFMS.Api.dll"]
