import { TransactionCard } from './TransactionCard'

import type { Transaction } from '../types/transaction'

interface Props {
  transactions: Transaction[]
  onDelete: (id: string) => void
}

export function TransactionList({
  transactions,
  onDelete,
}: Props) {
  return (
    <div className="space-y-4">
      {transactions.map(transaction => (
        <TransactionCard
          key={transaction.id}
          transaction={transaction}
          onDelete={onDelete}
        />
      ))}
    </div>
  )
}