using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.UseCases.Transactions.Commands.DeleteTransaction;

public class DeleteTransactionUseCase
{
    private readonly ITransactionRepository _repository;

    public DeleteTransactionUseCase(
        ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);

        if (transaction is null)
            return false;

        await _repository.DeleteAsync(transaction);

        return true;
    }
}