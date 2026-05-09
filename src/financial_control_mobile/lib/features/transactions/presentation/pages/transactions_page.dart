import 'package:flutter/material.dart';

import '../../data/api_service.dart';
import '../../data/models/create_transaction_request.dart';
import '../../domain/transaction_model.dart';

import '../widgets/create_transaction_dialog.dart';
import '../widgets/transaction_card.dart';

class TransactionsPage extends StatefulWidget {
  const TransactionsPage({super.key});

  @override
  State<TransactionsPage> createState() => _TransactionsPageState();
}

class _TransactionsPageState extends State<TransactionsPage> {
  final ApiService _apiService = ApiService();

  bool _isLoading = true;

  List<TransactionModel> _transactions = [];

  double get _total {
    return _transactions.fold(0, (sum, item) => sum + item.amount);
  }

  @override
  void initState() {
    super.initState();
    _loadTransactions();
  }

  Future<void> _loadTransactions() async {
    setState(() {
      _isLoading = true;
    });

    try {
      final transactions = await _apiService.getTransactions();

      if (!mounted) return;

      setState(() {
        _transactions = transactions;
      });
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Erro ao carregar transações: $e')),
      );
    } finally {
      if (!mounted) return;

      setState(() {
        _isLoading = false;
      });
    }
  }

  Future<void> _createTransaction(
    String description,
    double amount,
    String category,
    int type,
  ) async {
    try {
      await _apiService.createTransaction(
        CreateTransactionRequest(
          description: description,
          amount: amount,
          category: category,
          type: type,
          date: DateTime.now(),
        ),
      );

      await _loadTransactions();

      if (!mounted) return;

      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Transação criada com sucesso')),
      );
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Erro ao criar transação: $e')));
    }
  }

  Future<void> _updateTransaction(
    TransactionModel transaction,
    String description,
    double amount,
    String category,
    int type,
  ) async {
    try {
      await _apiService.updateTransaction(
        transaction.id,
        CreateTransactionRequest(
          description: description,
          amount: amount,
          category: category,
          type: type,
          date: DateTime.now(),
        ),
      );

      await _loadTransactions();

      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('Transação atualizada')));
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Erro ao atualizar: $e')));
    }
  }

  Future<void> _deleteTransaction(String id) async {
    try {
      await _apiService.deleteTransaction(id);

      await _loadTransactions();

      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(const SnackBar(content: Text('Transação removida')));
    } catch (e) {
      if (!mounted) return;

      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Erro ao remover: $e')));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          showDialog(
            context: context,
            builder: (_) {
              return CreateTransactionDialog(onSubmit: _createTransaction);
            },
          );
        },
        child: const Icon(Icons.add),
      ),

      body: SafeArea(
        child: Center(
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 430),

            child: Padding(
              padding: const EdgeInsets.all(20),

              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,

                children: [
                  const SizedBox(height: 12),

                  const Center(
                    child: Text(
                      'Financial Control',
                      style: TextStyle(
                        fontSize: 36,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),

                  const SizedBox(height: 32),

                  Card(
                    child: Padding(
                      padding: const EdgeInsets.all(24),

                      child: Column(
                        children: [
                          const Text(
                            'Saldo total',
                            style: TextStyle(fontSize: 16),
                          ),

                          const SizedBox(height: 12),

                          Text(
                            'R\$ ${_total.toStringAsFixed(2)}',

                            style: TextStyle(
                              fontSize: 32,
                              fontWeight: FontWeight.bold,

                              color: _total >= 0 ? Colors.green : Colors.red,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),

                  const SizedBox(height: 24),

                  Expanded(child: _buildContent()),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildContent() {
    if (_isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (_transactions.isEmpty) {
      return const Center(
        child: Text(
          'Nenhuma transação cadastrada',
          style: TextStyle(fontSize: 16),
        ),
      );
    }

    return RefreshIndicator(
      onRefresh: _loadTransactions,

      child: ListView.separated(
        itemCount: _transactions.length,

        separatorBuilder: (_, __) => const SizedBox(height: 16),

        itemBuilder: (context, index) {
          final transaction = _transactions[index];

          return TransactionCard(
            transaction: transaction,

            onDelete: () {
              _deleteTransaction(transaction.id);
            },

            onEdit: () {
              showDialog(
                context: context,

                builder: (_) {
                  return CreateTransactionDialog(
                    transaction: transaction,

                    onSubmit: (description, amount, category, type) {
                      _updateTransaction(
                        transaction,
                        description,
                        amount,
                        category,
                        type,
                      );
                    },
                  );
                },
              );
            },
          );
        },
      ),
    );
  }
}
