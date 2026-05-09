import 'dart:io';

import 'package:flutter/material.dart';
import 'package:window_size/window_size.dart';

import 'core/theme/app_theme.dart';
import 'features/transactions/presentation/pages/transactions_page.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();

  if (Platform.isLinux || Platform.isWindows || Platform.isMacOS) {
    setWindowTitle('Financial Control');

    setWindowMinSize(const Size(430, 900));
    setWindowMaxSize(const Size(430, 900));

    setWindowFrame(const Rect.fromLTWH(200, 100, 430, 900));
  }

  runApp(const FinancialControlApp());
}

class FinancialControlApp extends StatelessWidget {
  const FinancialControlApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Financial Control',
      debugShowCheckedModeBanner: false,
      theme: AppTheme.theme,
      home: const TransactionsPage(),
    );
  }
}
