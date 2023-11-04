using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using AcmePayService.Infrastructure.Data.DB;
using Microsoft.EntityFrameworkCore;
using AcmePayService.Domain;
using AcmePayService.Infrastructure.DAL.Repository.Implementation;
using AcmePayService.Infrastructure.DAL.Repository.Interfaces;
using AutoMapper;
using AcmePayService.Domain.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var appDBconnectionString = builder.Configuration.GetConnectionString("AppDB");
builder.Services.AddDbContext<AppDBContext>(x => x.UseSqlServer(appDBconnectionString));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ProjectRegistry).Assembly));
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AuthourizedPaymentRequestProfile());
    cfg.AddProfile(new CaptureAndVoidRequestProfile());
}).CreateMapper());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{Assembly.GetExecutingAssembly().GetName().Name}",
        Description = "Through this API, basic payment actions can be performed on AcmePAY e.g authorize, void and capture",
        Contact = new OpenApiContact
        {
            Name = "Ekechukwu Osu",
            Email = "ekechukwuosu@gmail.com"
        }
    });
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    options.IncludeXmlComments(xmlCommentsFullPath);
    options.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
});
var app = builder.Build();


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
