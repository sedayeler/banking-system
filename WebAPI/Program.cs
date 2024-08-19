using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Abstract;
using Repositories.Concrete;
using Services.Abstract;
using Services.Concrete;
using Services.Config;
using Services.DependencyResolvers.Autofac;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<ICustomerService, CustomerManager>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new AutofacServicesModel());
});

var app = builder.Build();

//AutoMapper Configuration
//builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();