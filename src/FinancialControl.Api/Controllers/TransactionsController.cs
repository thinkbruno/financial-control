using FinancialControl.Application.UseCases.Transactions;
using FinancialControl.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly GetAllTransactionsUseCase _getAllTransactionsUseCase;
    private readonly CreateTransactionUseCase _createTransactionUseCase;

    public TransactionsController(
     CreateTransactionUseCase createTransactionUseCase,
     GetAllTransactionsUseCase getAllTransactionsUseCase)
    {
        _createTransactionUseCase = createTransactionUseCase;
        _getAllTransactionsUseCase = getAllTransactionsUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _getAllTransactionsUseCase.Execute();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionInput input)
    {
        if (input == null)
            return BadRequest("Dados da transação inválidos.");

        var transaction = await _createTransactionUseCase.Execute(input);

        return CreatedAtAction(nameof(GetAll), new { id = transaction.Id }, transaction);
    }
}