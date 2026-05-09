class TransactionModel {
  final String id;
  final String description;
  final double amount;
  final String category;
  final String type;
  final DateTime date;

  TransactionModel({
    required this.id,
    required this.description,
    required this.amount,
    required this.category,
    required this.type,
    required this.date,
  });

  factory TransactionModel.fromJson(Map<String, dynamic> json) {
    return TransactionModel(
      id: json['id'],
      description: json['description'],
      amount: (json['amount'] as num).toDouble(),
      category: json['category'],
      type: json['type'].toString(),
      date: DateTime.parse(json['date']),
    );
  }
}
