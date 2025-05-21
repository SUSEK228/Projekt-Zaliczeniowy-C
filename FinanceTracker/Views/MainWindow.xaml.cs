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
                TypeComboBox.SelectedIndex >= 0)
            {
                var type = TypeComboBox.SelectedIndex == 0
                    ? Transaction.TransactionType.Income
                    : Transaction.TransactionType.Expense;

                if (type == TransactionType.Expense && !transactionManager.CanAddExpense(amount))
                {
                    MessageBox.Show("Przekroczono limit wydatków na ten miesiąc!", "Limit", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } 

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
                UpdateStatistics();

                // Wyczyść formularz
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
                    UpdateStatistics(); 
                }
            }
        }

        private void UpdateBalance()
        {
            BalanceTextBlock.Text = $"Saldo: {transactionManager.GetBalance():C}";
        }


        // **NOWE** DO AKTUALIZACJI STATYSTYK
        private void UpdateStatistics()
        {
            var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            var count = transactions.Count;

            TotalIncomeText.Text = $"💰 Przychody: {income:C}";
            TotalExpenseText.Text = $"💸 Wydatki: {expense:C}";
            TotalTransactionsText.Text = $"📊 Liczba transakcji: {count}";
        }

        private void ShowBudgetView(object sender, RoutedEventArgs e) 
        {
            BudgetGrid.Visibility = Visibility.Visible;
            StatsGrid.Visibility = Visibility.Collapsed;
            LimitsGrid.Visibility = Visibility.Collapsed;
        }

        private void ShowStatsView(object sender, RoutedEventArgs e)
        {
            BudgetGrid.Visibility = Visibility.Collapsed;
            StatsGrid.Visibility = Visibility.Visible;
            LimitsGrid.Visibility = Visibility.Collapsed;
        }
        private void ShowLimitsView(object sender, RoutedEventArgs e)
        {
            BudgetGrid.Visibility = Visibility.Collapsed;
            StatsGrid.Visibility = Visibility.Collapsed;
            LimitsGrid.Visibility = Visibility.Visible;
        }

        private void FilterTransactions_Click(object sender, RoutedEventArgs e)
        {
            var filtered = transactionManager.GetAllTransactions();

            // Filtruj po opisie
            if (!string.IsNullOrWhiteSpace(FilterDescriptionTextBox.Text))
            {
                filtered = filtered.Where(t => t.Description.Contains(FilterDescriptionTextBox.Text, StringComparison.OrdinalIgnoreCase));
            }


            // Filtruj po dacie
            if (FilterDatePicker.SelectedDate.HasValue)
            {
                var selectedDate = FilterDatePicker.SelectedDate.Value.Date;
                filtered = filtered.Where(t => t.Date.Date == selectedDate);
            }

            transactions.Clear();
            foreach (var t in filtered)
                transactions.Add(t);

            UpdateBalance();
            UpdateStatistics();
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            FilterDescriptionTextBox.Text = "";
            FilterDatePicker.SelectedDate = null;

            transactions.Clear();
            foreach (var t in transactionManager.GetAllTransactions())
                transactions.Add(t);

            UpdateBalance();
            UpdateStatistics();
        }

        private void SetLimit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (decimal.TryParse(LimitAmountTextBox.Text, out decimal amount) &&
                    LimitDatePicker.SelectedDate.HasValue)
                {
                    using var context = new FinanceContext();

                    var selected = LimitDatePicker.SelectedDate.Value;
                    var month = new DateTime(selected.Year, selected.Month, 1);

                    var existing = context.SpendingLimits
                        .FirstOrDefault(l => l.Month.Year == month.Year && l.Month.Month == month.Month);

                    if (existing != null)
                    {
                        existing.LimitAmount = amount;
                    }
                    else
                    {
                        context.SpendingLimits.Add(new SpendingLimit
                        {
                            LimitAmount = amount,
                            Month = month
                        });
                    }

                    context.SaveChanges();
                    MessageBox.Show("Limit zapisano!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Podaj poprawną kwotę i miesiąc!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}", "Wyjątek", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





    }
}

