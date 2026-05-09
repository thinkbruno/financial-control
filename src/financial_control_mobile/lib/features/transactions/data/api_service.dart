import 'dart:convert';

import 'package:http/http.dart' as http;

import '../domain/transaction_model.dart';
import 'models/create_transaction_request.dart';

class ApiService {
  final String baseUrl = 'http://localhost:8080/api/transactions';

  Future<List<TransactionModel>> getTransactions() async {
    final response = await http.get(Uri.parse(baseUrl));

    if (response.statusCode != 200) {
      throw Exception('Erro ao carregar transações');
    }

    final List<dynamic> data = jsonDecode(response.body);

    return data.map((json) => TransactionModel.fromJson(json)).toList();
  }

  Future<void> createTransaction(CreateTransactionRequest request) async {
    final response = await http.post(
      Uri.parse(baseUrl),

      headers: {'Content-Type': 'application/json'},

      body: jsonEncode(request.toJson()),
    );

    if (response.statusCode != 201 && response.statusCode != 200) {
      throw Exception('Erro ao criar transação');
    }
  }

  Future<void> updateTransaction(
    String id,
    CreateTransactionRequest request,
  ) async {
    final response = await http.put(
      Uri.parse('$baseUrl/$id'),

      headers: {'Content-Type': 'application/json'},

      body: jsonEncode(request.toJson()),
    );

    if (response.statusCode != 200) {
      throw Exception('Erro ao atualizar transação');
    }
  }

  Future<void> deleteTransaction(String id) async {
    final response = await http.delete(Uri.parse('$baseUrl/$id'));

    if (response.statusCode != 204 && response.statusCode != 200) {
      throw Exception('Erro ao deletar transação');
    }
  }
}
