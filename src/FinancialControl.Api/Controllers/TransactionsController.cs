using FinancialControl.Application.UseCases.Transactions.Commands.CreateTransaction;
using FinancialControl.Application.UseCases.Transactions.Commands.DeleteTransaction;
using FinancialControl.Application.UseCases.Transactions.Commands.UpdateTransaction;
using FinancialControl.Application.UseCases.Transactions.Queries.GetAllTransactions;
using FinancialControl.Application.UseCases.Transactions.Queries.GetTransactionById;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly CreateTransactionUseCase _createTransactionUseCase;

    private readonly GetAllTransactionsUseCase _getAllTransactionsUseCase;

    private readonly GetTransactionByIdUseCase _getTransactionByIdUseCase;

    private readonly UpdateTransactionUseCase _updateTransactionUseCase;

    private readonly DeleteTransactionUseCase _deleteTransactionUseCase;

    public TransactionsController(
        CreateTransactionUseCase createTransactionUseCase,
        GetAllTransactionsUseCase getAllTransactionsUseCase,
        GetTransactionByIdUseCase getTransactionByIdUseCase,
        UpdateTransactionUseCase updateTransactionUseCase,
        DeleteTransactionUseCase deleteTransactionUseCase)
    {
        _createTransactionUseCase = createTransactionUseCase;

        _getAllTransactionsUseCase = getAllTransactionsUseCase;

        _getTransactionByIdUseCase = getTransactionByIdUseCase;

        _updateTransactionUseCase = updateTransactionUseCase;

        _deleteTransactionUseCase = deleteTransactionUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _getAllTransactionsUseCase.ExecuteAsync();

        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await _getTransactionByIdUseCase.ExecuteAsync(id);

        if (transaction is null)
        {
            return NotFound(new
            {
                message = "Transaction not found"
            });
        }

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTransactionInput input)
    {
        var transaction = await _createTransactionUseCase.ExecuteAsync(input);

        return CreatedAtAction(
            nameof(GetById),
            new { id = transaction.Id },
            transaction);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateTransactionInput input)
    {
        var success = await _updateTransactionUseCase.ExecuteAsync(id, input);

        if (!success)
        {
            return NotFound(new
            {
                message = "Transaction not found"
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _deleteTransactionUseCase.ExecuteAsync(id);

        if (!success)
        {
            return NotFound(new
            {
                message = "Transaction not found"
            });
        }

        return NoContent();
    }
}