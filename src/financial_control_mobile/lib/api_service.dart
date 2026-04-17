import 'package:dio/dio.dart';
import 'transaction_model.dart';

class ApiService {
  final _dio = Dio(BaseOptions(baseUrl: 'http://localhost:5101/api'));

  Future<List<TransactionModel>> getTransactions() async {
    try {
      final response = await _dio.get('/Transactions');
      final List data = response.data;
      return data.map((e) => TransactionModel.fromJson(e)).toList();
    } catch (e) {
      throw Exception('Erro ao carregar dados: $e');
    }
  }
}
