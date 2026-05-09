import 'package:flutter/material.dart';

import '../../domain/transaction_model.dart';

class TransactionCard extends StatelessWidget {
  final TransactionModel transaction;

  final VoidCallback onEdit;
  final VoidCallback onDelete;

  const TransactionCard({
    super.key,
    required this.transaction,
    required this.onEdit,
    required this.onDelete,
  });

  bool get isExpense => transaction.type == 2;

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(18),

        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [
            Row(
              children: [
                Expanded(
                  child: Text(
                    transaction.description,

                    style: const TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),

                PopupMenuButton<String>(
                  onSelected: (value) {
                    if (value == 'edit') {
                      onEdit();
                    }

                    if (value == 'delete') {
                      onDelete();
                    }
                  },

                  itemBuilder: (context) => [
                    const PopupMenuItem(value: 'edit', child: Text('Editar')),

                    const PopupMenuItem(
                      value: 'delete',
                      child: Text('Excluir'),
                    ),
                  ],
                ),
              ],
            ),

            const SizedBox(height: 12),

            Text(
              transaction.category,
              style: TextStyle(color: Colors.grey.shade700),
            ),

            const SizedBox(height: 16),

            Text(
              'R\$ ${transaction.amount.toStringAsFixed(2)}',

              style: TextStyle(
                fontSize: 22,
                fontWeight: FontWeight.bold,

                color: isExpense ? Colors.red : Colors.green,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
