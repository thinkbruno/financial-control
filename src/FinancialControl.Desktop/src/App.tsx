import { useEffect, useState } from 'react';
import { api } from './services/api';

// Importação de tipos (necessário devido ao verbatimModuleSyntax)
import type { AxiosResponse, AxiosError } from 'axios';

// Interface tipada conforme seu domínio C#
interface Transaction {
  id: string;
  description: string;
  amount: number;
  type: 'Income' | 'Expense';
  date: string;
  category: string;
}

export default function App() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    // Busca os dados usando a instância configurada com .env
    api.get<Transaction[]>('/Transactions')
      .then((response: AxiosResponse<Transaction[]>) => {
        setTransactions(response.data);
      })
      .catch((err: AxiosError) => {
        console.error("Erro ao carregar transações:", err.message);
      })
      .finally(() => {
        setLoading(setLoading(false) as unknown as boolean); // Garantindo o fim do loading
        setLoading(false);
      });
  }, []);

  // Cálculo de saldo simplificado
  const balance = transactions.reduce((acc, tr) => 
    tr.type === 'Income' ? acc + tr.amount : acc - tr.amount, 0);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-slate-900 text-emerald-400">
        <p className="text-xl font-mono animate-pulse">Carregando dados da API...</p>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-900 text-slate-100 p-4 md:p-8">
      <div className="max-w-5xl mx-auto">
        
        {/* Header Responsivo */}
        <header className="flex flex-col md:flex-row md:justify-between md:items-center gap-6 mb-12 border-b border-slate-800 pb-8">
          <div>
            <h1 className="text-3xl font-bold text-emerald-400 tracking-tight">FinancialControl.io</h1>
            <p className="text-slate-400 text-sm mt-1">Gestão de Fluxo de Caixa</p>
          </div>
          
          <div className="bg-slate-800/50 p-4 rounded-lg border border-slate-700 min-w-[200px]">
            <p className="text-slate-400 text-xs uppercase tracking-wider mb-1">Saldo Atual</p>
            <p className={`text-2xl font-mono font-bold ${balance >= 0 ? 'text-emerald-400' : 'text-rose-400'}`}>
              {balance.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
            </p>
          </div>
        </header>

        {/* Tabela de Transações */}
        <main className="bg-slate-800 rounded-xl overflow-hidden border border-slate-700 shadow-2xl">
          <div className="p-6 border-b border-slate-700 flex justify-between items-center">
            <h2 className="text-xl font-semibold">Movimentações</h2>
            <span className="text-xs bg-slate-700 px-3 py-1 rounded-full text-slate-300">
              {transactions.length} registros
            </span>
          </div>
          
          <div className="overflow-x-auto">
            <table className="w-full text-left">
              <thead className="bg-slate-900/50 text-slate-500 text-xs uppercase">
                <tr>
                  <th className="px-6 py-4 font-medium">Descrição</th>
                  <th className="px-6 py-4 font-medium text-right">Valor</th>
                  <th className="px-6 py-4 font-medium">Data</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-slate-700/50">
                {transactions.length > 0 ? (
                  transactions.map(tr => (
                    <tr key={tr.id} className="hover:bg-slate-700/30 transition-colors">
                      <td className="px-6 py-4">
                        <p className="text-slate-200 font-medium">{tr.description}</p>
                        <p className="text-slate-500 text-xs">{tr.category}</p>
                      </td>
                      <td className={`px-6 py-4 text-right font-mono font-bold ${tr.type === 'Income' ? 'text-emerald-400' : 'text-rose-400'}`}>
                        {tr.type === 'Income' ? '+' : '-'} {tr.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
                      </td>
                      <td className="px-6 py-4 text-slate-400 text-sm">
                        {new Date(tr.date).toLocaleDateString('pt-BR')}
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan={3} className="px-6 py-12 text-center text-slate-500 italic">
                      Nenhuma transação encontrada.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </main>
      </div>
    </div>
  );
}