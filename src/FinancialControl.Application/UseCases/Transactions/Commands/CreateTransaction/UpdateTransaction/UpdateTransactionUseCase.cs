using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.UseCases.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionUseCase
{
    private readonly ITransactionRepository _repository;

    public UpdateTransactionUseCase(
        ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(
        Guid id,
        UpdateTransactionInput input)
    {
        var transaction = await _repository.GetByIdAsync(id);

        if (transaction is null)
            return false;

        transaction.Update(
            input.Description,
            input.Amount,
            input.Date,
            input.Type,
            input.Category);

        await _repository.UpdateAsync(transaction);

        return true;
    }
}