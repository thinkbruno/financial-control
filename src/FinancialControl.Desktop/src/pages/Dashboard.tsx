import { TransactionForm } from '../components/TransactionForm'
import { TransactionList } from '../components/TransactionList'

import { useTransactions } from '../hooks/useTransactions'

export function Dashboard() {
  const {
    transactions,
    loading,
    createTransaction,
    deleteTransaction,
  } = useTransactions()

  const total = transactions.reduce(
    (accumulator, transaction) =>
      accumulator + transaction.amount,
    0,
  )

  return (
    <div className="min-h-screen bg-zinc-950 text-zinc-100">
      <div className="max-w-6xl mx-auto p-8">
        <header className="mb-10">
          <h1 className="text-5xl font-bold">
            Financial Control
          </h1>

          <p className="text-zinc-400 mt-2">
            Gerencie suas receitas e despesas
          </p>
        </header>

        <div className="grid grid-cols-3 gap-4 mb-8">
          <div className="bg-zinc-900 border border-zinc-800 rounded-2xl p-6">
            <span className="text-zinc-400 text-sm">
              Total
            </span>

            <h2 className="text-3xl font-bold mt-2">
              R$ {total.toFixed(2)}
            </h2>
          </div>

          <div className="bg-green-950 border border-green-800 rounded-2xl p-6">
            <span className="text-green-400 text-sm">
              Receitas
            </span>

            <h2 className="text-3xl font-bold mt-2">
              R${' '}
              {transactions
                .filter(t => t.type === 'Income')
                .reduce((acc, t) => acc + t.amount, 0)
                .toFixed(2)}
            </h2>
          </div>

          <div className="bg-red-950 border border-red-800 rounded-2xl p-6">
            <span className="text-red-400 text-sm">
              Despesas
            </span>

            <h2 className="text-3xl font-bold mt-2">
              R${' '}
              {Math.abs(
                transactions
                  .filter(t => t.type === 'Expense')
                  .reduce((acc, t) => acc + t.amount, 0),
              ).toFixed(2)}
            </h2>
          </div>
        </div>

        <TransactionForm onCreate={createTransaction} />

        <div className="mt-8">
          {loading ? (
            <p className="text-zinc-400">
              Carregando transações...
            </p>
          ) : (
            <TransactionList
              transactions={transactions}
              onDelete={deleteTransaction}
            />
          )}
        </div>
      </div>
    </div>
  )
}