using CFMS.Api.Extensions;
using CFMS.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCorsPolicy();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation(); 

app.UseHttpsRedirection();

app.UseRouting();

app.UseCorsPolicy(); 

app.UseAuthentication(); 

app.UseMiddleware<ErrorHandlingMiddleware>(); 
app.UseMiddleware<JwtBlacklistMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

