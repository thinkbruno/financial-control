import 'package:flutter/material.dart';

import '../../domain/transaction_model.dart';

class CreateTransactionDialog extends StatefulWidget {
  final Function(String description, double amount, String category, int type)
  onSubmit;

  final TransactionModel? transaction;

  const CreateTransactionDialog({
    super.key,
    required this.onSubmit,
    this.transaction,
  });

  @override
  State<CreateTransactionDialog> createState() =>
      _CreateTransactionDialogState();
}

class _CreateTransactionDialogState extends State<CreateTransactionDialog> {
  late final TextEditingController _descriptionController;

  late final TextEditingController _amountController;

  late final TextEditingController _categoryController;

  late int _type;

  bool get isEditing => widget.transaction != null;

  @override
  void initState() {
    super.initState();

    final transaction = widget.transaction;

    _descriptionController = TextEditingController(
      text: transaction?.description ?? '',
    );

    _amountController = TextEditingController(
      text: transaction != null ? transaction.amount.abs().toString() : '',
    );

    _categoryController = TextEditingController(
      text: transaction?.category ?? '',
    );

    _type = transaction?.type.toLowerCase() == 'expense' ? 2 : 1;
  }

  @override
  void dispose() {
    _descriptionController.dispose();
    _amountController.dispose();
    _categoryController.dispose();

    super.dispose();
  }

  void _submit() {
    final description = _descriptionController.text.trim();

    final amount = double.tryParse(_amountController.text.replaceAll(',', '.'));

    final category = _categoryController.text.trim();

    if (description.isEmpty) {
      _showError('Informe uma descrição');
      return;
    }

    if (amount == null || amount <= 0) {
      _showError('Informe um valor válido');
      return;
    }

    if (category.isEmpty) {
      _showError('Informe uma categoria');
      return;
    }

    widget.onSubmit(description, amount, category, _type);

    Navigator.pop(context);
  }

  void _showError(String message) {
    ScaffoldMessenger.of(
      context,
    ).showSnackBar(SnackBar(content: Text(message)));
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(isEditing ? 'Editar transação' : 'Nova transação'),

      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,

          children: [
            TextField(
              controller: _descriptionController,

              decoration: const InputDecoration(labelText: 'Descrição'),
            ),

            const SizedBox(height: 16),

            TextField(
              controller: _amountController,

              keyboardType: const TextInputType.numberWithOptions(
                decimal: true,
              ),

              decoration: const InputDecoration(labelText: 'Valor'),
            ),

            const SizedBox(height: 16),

            TextField(
              controller: _categoryController,

              decoration: const InputDecoration(labelText: 'Categoria'),
            ),

            const SizedBox(height: 16),

            DropdownButtonFormField<int>(
              value: _type,

              decoration: const InputDecoration(labelText: 'Tipo'),

              items: const [
                DropdownMenuItem(value: 1, child: Text('Receita')),

                DropdownMenuItem(value: 2, child: Text('Despesa')),
              ],

              onChanged: (value) {
                setState(() {
                  _type = value ?? 1;
                });
              },
            ),
          ],
        ),
      ),

      actions: [
        TextButton(
          onPressed: () => Navigator.pop(context),

          child: const Text('Cancelar'),
        ),

        ElevatedButton(
          onPressed: _submit,

          child: Text(isEditing ? 'Salvar' : 'Criar'),
        ),
      ],
    );
  }
}
