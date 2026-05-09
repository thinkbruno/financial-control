class CreateTransactionRequest {
  final String description;
  final double amount;
  final String category;
  final int type;
  final DateTime date;

  CreateTransactionRequest({
    required this.description,
    required this.amount,
    required this.category,
    required this.type,
    required this.date,
  });

  Map<String, dynamic> toJson() {
    return {
      'description': description,
      'amount': amount,
      'category': category,
      'type': type,
      'date': date.toIso8601String(),
    };
  }
}
