using CFMS.Api.Extensions;
using CFMS.Application.Behaviors;
using CFMS.Application.Common;
using CFMS.Application.DTOs.Auth;
using CFMS.Application.Events;
using CFMS.Application.Features.UserFeat.Auth;
using CFMS.Application.Services.Impl;
using CFMS.Application.Services;
using CFMS.Domain.Interfaces;
using CFMS.Infrastructure.Persistence;
using CFMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCorsPolicy();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCorsPolicy();
app.UseAuthorization();

app.MapControllers();
app.Run();
