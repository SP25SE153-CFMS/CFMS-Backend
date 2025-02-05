using CFMS.Api.Extensions;
using CFMS.Application.Behaviors;
using CFMS.Application.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCorsPolicy();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(EventQueueBehavior<,>));
builder.Services.AddSingleton<EventQueue>();
builder.Services.AddMediatRServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCorsPolicy();  
app.UseAuthorization();

app.MapControllers();
app.Run();
