using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.UseCases.Transactions;

public class GetAllTransactionsUseCase
{
    private readonly ITransactionRepository _repository;

    public GetAllTransactionsUseCase(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Transaction>> Execute()
    {
        return await _repository.GetAllAsync();
    }
}