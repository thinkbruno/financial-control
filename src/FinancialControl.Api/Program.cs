using System.Text.Json;
using System.Text.Json.Serialization;
using FinancialControl.Application.UseCases.Transactions;
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