# 23.04.2025
- Wprowadzenie Notatek
- Dodano plik FinanceCOntext.cs w folderze Models
- Zostało skonfigurowane połączenie z bazą danych sqlite "finances.db"
- W klasie Transcation dodano Id jako klucz głowny
- W TransactionManager: zmieniono metody AddTransatcion() i GetAllTransactions() tak by działały na bazie danych
- Dodano metodę InitializeDatabase()
- GetBalance() działa teraz na podstawie bazy danych

- W MainWindow.xaml:
- Dodano przycisk "Kosz", który służy do usuwania transakcji
- W TransactionManager.cs została dodana metoda, która obsługuje ten kosz

- W MainWindow.xaml.cs:
- Dodano metodę DeleteTransaction_Click(), która usuwa transakcje i usuwa ją z ObservableCollection oraz odświeża saldo
- W konstruktorze MainWindow wywoływana jest InitializeDatabase() oraz dane są pobierane z bazy i przypisywane do Transactions.Data.Grid.ItemsSource