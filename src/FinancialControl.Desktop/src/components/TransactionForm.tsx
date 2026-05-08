import { useState } from 'react'

import type {
  CreateTransactionDTO,
  TransactionType,
} from '../types/transaction'

interface Props {
  onCreate: (data: CreateTransactionDTO) => Promise<void>
}

export function TransactionForm({
  onCreate,
}: Props) {
  const [description, setDescription] = useState('')
  const [amount, setAmount] = useState('')
  const [category, setCategory] = useState('')
  const [date, setDate] = useState('')
  const [type, setType] =
    useState<TransactionType>('Expense')

  async function handleSubmit(
    event: React.FormEvent<HTMLFormElement>,
  ) {
    event.preventDefault()

    await onCreate({
      description,
      amount: Number(amount),
      category,
      date,
      type,
    })

    setDescription('')
    setAmount('')
    setCategory('')
    setDate('')
    setType('Expense')
  }

  return (
    <form
      onSubmit={handleSubmit}
      className="bg-zinc-900 border border-zinc-800 rounded-xl p-6 mb-8 space-y-4"
    >
      <h2 className="text-2xl font-bold">
        Nova Transação
      </h2>

      <input
        type="text"
        placeholder="Descrição"
        value={description}
        onChange={event =>
          setDescription(event.target.value)
        }
        className="w-full bg-zinc-800 rounded-lg p-3"
      />

      <input
        type="number"
        placeholder="Valor"
        value={amount}
        onChange={event =>
          setAmount(event.target.value)
        }
        className="w-full bg-zinc-800 rounded-lg p-3"
      />

      <input
        type="text"
        placeholder="Categoria"
        value={category}
        onChange={event =>
          setCategory(event.target.value)
        }
        className="w-full bg-zinc-800 rounded-lg p-3"
      />

      <input
        type="date"
        value={date}
        onChange={event =>
          setDate(event.target.value)
        }
        className="w-full bg-zinc-800 rounded-lg p-3"
      />

      <select
        value={type}
        onChange={event =>
          setType(event.target.value as TransactionType)
        }
        className="w-full bg-zinc-800 rounded-lg p-3"
      >
        <option value="Income">Receita</option>

        <option value="Expense">Despesa</option>
      </select>

      <button
        type="submit"
        className="bg-green-500 hover:bg-green-400 text-black font-bold px-4 py-3 rounded-lg"
      >
        Salvar
      </button>
    </form>
  )
}