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

        // Log estruturado: facilita buscas em ferramentas como ElasticSearch ou Seq no futuro
        _logger.LogInformation("Processando Transação Financeira: {TransactionId} | Descrição: {Description} | Valor: {Amount}",
            message.TransactionId, message.Description, message.Amount);

        await Task.Delay(500); // Simulando trabalho

        _logger.LogInformation("✅ Sucesso: Transação {TransactionId} integrada ao sistema.", message.TransactionId);
    }
}