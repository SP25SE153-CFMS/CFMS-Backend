using CFMS.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCorsPolicy();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

var app = builder.Build();

app.UseSwaggerDocumentation();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCorsPolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.Run();
