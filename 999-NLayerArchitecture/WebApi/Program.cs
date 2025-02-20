using Application.DependencyInjection.AspectCore;
using Application.Interfaces;
using Application.Services;
using Application.Validation.FluentValidation;
using AspectCore.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


//Aspect Core DI Dependency Inject Services
builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
builder.Services.AddAspectCoreServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
