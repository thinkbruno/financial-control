using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialControl.Application.UseCases.Transactions.Queries.GetAllTransactions;

public class GetAllTransactionsUseCase
{
    private readonly ITransactionRepository _repository;

    public GetAllTransactionsUseCase(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Transaction>> Execute()
    {
        return await _repository.GetAllAsync();
    }
}