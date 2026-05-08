import type { Transaction } from '../types/transaction'

interface Props {
  transaction: Transaction
  onDelete: (id: string) => void
}

export function TransactionCard({
  transaction,
  onDelete,
}: Props) {
  return (
    <div className="bg-zinc-900 border border-zinc-800 rounded-xl p-4">
      <div className="flex items-center justify-between">
        <div>
          <h2 className="text-lg font-semibold">
            {transaction.description}
          </h2>

          <p className="text-sm text-zinc-400">
            {transaction.category}
          </p>
        </div>

        <div className="flex items-center gap-4">
          <strong
            className={
              transaction.type === 'Income'
                ? 'text-green-400'
                : 'text-red-400'
            }
          >
            R$ {transaction.amount}
          </strong>

          <button
            onClick={() => onDelete(transaction.id)}
            className="text-red-400 hover:text-red-300"
          >
            Excluir
          </button>
        </div>
      </div>
    </div>
  )
}