using FinancialControl.Worker.Consumers;
using MassTransit;
using Serilog;

// Configura o Serilog antes de tudo
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = Host.CreateApplicationBuilder(args);

// Usa o Serilog como provedor de logs
builder.Services.AddSerilog();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TransactionCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("transaction-created-queue", e =>
        {
            e.ConfigureConsumer<TransactionCreatedConsumer>(context);
        });
    });
});

try
{
    Log.Information("Iniciando o Worker de Controle Financeiro...");
    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "O Worker falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush();
}