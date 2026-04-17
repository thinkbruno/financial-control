using System.Text.Json;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Context;
using FinancialControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using FluentValidation;
using FluentValidation.AspNetCore;
using FinancialControl.Domain.Validators;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<FinancialDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionValidator>();


var reactUrl = builder.Configuration["FrontendUrl"] ?? "http://localhost:5173";
var flutterUrl = "http://localhost:5174";

builder.Services.AddCors(options =>
{
    options.AddPolicy("MultiPlatformPolicy", policy =>
    {
        policy.WithOrigins(reactUrl, flutterUrl)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("MultiPlatformPolicy");




app.UseAuthorization();
app.MapControllers();

app.Run();