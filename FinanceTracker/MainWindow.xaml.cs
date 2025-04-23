using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FinanceTracker.Models;
using static FinanceTracker.Models.Transaction;

namespace FinanceTracker
{
    public partial class MainWindow : Window
    {
        private TransactionManager transactionManager = new();
        private ObservableCollection<Transaction> transactions = new();

        public MainWindow()
        {
            InitializeComponent();
            transactionManager.InitializeDatabase(); // *NOWE*

            transactions = new ObservableCollection<Transaction>(transactionManager.GetAllTransactions()); // *NOWE* została dodana nowa pusta lista na transkacje do baz danych
            TransactionsDataGrid.ItemsSource = transactions;// *NOWE 

            UpdateBalance();
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(AmountTextBox.Text, out decimal amount) &&
                TypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                var type = selectedItem.Content.ToString() == "Przychód"
                    ? TransactionType.Income
                    : TransactionType.Expense;

                var transaction = new Transaction
                {
                    Description = DescriptionTextBox.Text,
                    Amount = amount,
                    Date = DateTime.Now,
                    Type = type
                };

                transactionManager.AddTransaction(transaction);
                transactions.Add(transaction);
                UpdateBalance();

                // Czyść formularz
                DescriptionTextBox.Text = "";
                AmountTextBox.Text = "";
                TypeComboBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Podaj poprawną kwotę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Transaction transaction)
            {
                var result = MessageBox.Show("Czy na pewno chcesz usunąć tę transakcję?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    transactionManager.DeleteTransaction(transaction);
                    transactions.Remove(transaction);
                    UpdateBalance();
                }
            }
        }

        private void UpdateBalance()
        {
            BalanceTextBlock.Text = $"Saldo: {transactionManager.GetBalance():C}";
        }


    }
}
