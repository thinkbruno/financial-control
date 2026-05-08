import { useEffect, useState } from 'react'

import { api } from '../api/api'

import type {
    CreateTransactionDTO,
    Transaction,
    UpdateTransactionDTO,
} from '../types/transaction'

export function useTransactions() {
    const [transactions, setTransactions] = useState<Transaction[]>([])
    const [loading, setLoading] = useState(true)

    async function loadTransactions() {
        try {
            const response = await api.get('/transactions')

            setTransactions(response.data)
        } finally {
            setLoading(false)
        }
    }

    async function createTransaction(
        data: CreateTransactionDTO,
    ) {
        const response = await api.post('/transactions', data)

        setTransactions(previous => [
            response.data,
            ...previous,
        ])
    }

    async function updateTransaction(
        id: string,
        data: UpdateTransactionDTO,
    ) {
        await api.put(`/transactions/${id}`, data)

        await loadTransactions()
    }

    async function deleteTransaction(id: string) {
        await api.delete(`/transactions/${id}`)

        setTransactions(previous =>
            previous.filter(transaction => transaction.id !== id),
        )
    }

    useEffect(() => {
        loadTransactions()
    }, [])

    return {
        transactions,
        loading,
        createTransaction,
        updateTransaction,
        deleteTransaction,
    }
}