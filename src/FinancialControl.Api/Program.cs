using System.Text.Json;
using System.Text.Json.Serialization;
using FinancialControl.Application.UseCases.Transactions.Commands.CreateTransaction;
using FinancialControl.Application.UseCases.Transactions.Queries.GetAllTransactions;
using FinancialControl.Application.UseCases.Transactions.Commands.UpdateTransaction;
using FinancialControl.Application.UseCases.Transactions.Commands.DeleteTransaction;
using FinancialControl.Application.UseCases.Transactions.Queries.GetTransactionById;
using FinancialControl.Infrastructure;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure (DbContext, Repository, EventPublisher)
builder.Services.AddInfrastructure(builder.Configuration);

// UseCases
builder.Services.AddScoped<CreateTransactionUseCase>();
builder.Services.AddScoped<GetAllTransactionsUseCase>();
builder.Services.AddScoped<UpdateTransactionUseCase>();
builder.Services.AddScoped<DeleteTransactionUseCase>();
builder.Services.AddScoped<GetTransactionByIdUseCase>();

// CORS
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

// MassTransit (RabbitMQ)
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
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