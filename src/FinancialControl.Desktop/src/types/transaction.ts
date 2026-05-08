export type TransactionType = 'Income' | 'Expense'

export interface Transaction {
    id: string
    description: string
    amount: number
    date: string
    type: TransactionType
    category: string
    createdAt: string
}

export interface CreateTransactionDTO {
    description: string
    amount: number
    date: string
    type: TransactionType
    category: string
}

export interface UpdateTransactionDTO {
    description: string
    amount: number
    date: string
    type: TransactionType
    category: string
}