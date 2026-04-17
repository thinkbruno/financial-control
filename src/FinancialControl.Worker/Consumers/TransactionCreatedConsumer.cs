using FinancialControl.Domain.Events;
using MassTransit;

namespace FinancialControl.Worker.Consumers;

public class TransactionCreatedConsumer : IConsumer<TransactionCreatedEvent>
{
    private readonly ILogger<TransactionCreatedConsumer> _logger;

    public TransactionCreatedConsumer(ILogger<TransactionCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
    {
        var message = context.Message;

        // lógica de negócio (Ex: Atualizar saldo no banco)
        _logger.LogInformation("Recebendo transação: {Id} - {Description} no valor de R$ {Amount}",
            message.Id, message.Description, message.Amount);

        // Simulando um processamento pesado
        await Task.Delay(1000);

        _logger.LogInformation("Transação {Id} processada com sucesso!", message.Id);
    }
}