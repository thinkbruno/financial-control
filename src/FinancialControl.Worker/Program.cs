using FinancialControl.Worker.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

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

var host = builder.Build();
host.Run();