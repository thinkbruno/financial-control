using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.UseCases.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdUseCase
{
    private readonly ITransactionRepository _repository;

    public GetTransactionByIdUseCase(
        ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Transaction?> ExecuteAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}