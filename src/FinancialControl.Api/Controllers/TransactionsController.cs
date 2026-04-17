using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Domain.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public TransactionsController(
        ITransactionRepository repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _repository.GetAllAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Transaction transaction)
    {
        if (transaction == null)
            return BadRequest("Dados da transação inválidos.");


        transaction.Id = Guid.NewGuid();

        // Persistência no Banco de Dados (Postgres)
        await _repository.AddAsync(transaction);

        // Publicação do evento para o RabbitMQ (Worker)
        await _publishEndpoint.Publish(new TransactionCreatedEvent(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Type.ToString()
        ));

        return CreatedAtAction(nameof(GetAll), new { id = transaction.Id }, transaction);
    }
}