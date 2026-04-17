using FinancialControl.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Repositories;
using MassTransit;
using FluentValidation;
using FluentValidation.AspNetCore;
using FinancialControl.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionValidator>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<FinancialDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });


        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();

app.Run();