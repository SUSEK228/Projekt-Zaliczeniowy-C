using System.Collections.Generic;
using System.Linq;
using static FinanceTracker.MainWindow;
using static FinanceTracker.Models.Transaction;

namespace FinanceTracker.Models
{
    public class TransactionManager
    {

        public void AddTransaction(Transaction transaction) // Dodaje transakcje do listy
        {
            using var context = new FinanceContext();
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction) // *NOWE* Usuwa transakcje
        {
            using var context = new FinanceContext();
            var toRemove = context.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (toRemove != null)
            {
                context.Transactions.Remove(toRemove);
                context.SaveChanges();
            }
        }

        public IEnumerable<Transaction> GetAllTransactions()  // Zwraca całą listę transakcji
        {
            using var context = new FinanceContext();
            return context.Transactions.ToList();
        }

        public decimal GetBalance() // Oblicza aktualne saldo // *ZMIANA* teraz liczy saldo bezposrednio z bazdy danych
        {
            using var context = new FinanceContext();
            return context.Transactions 
               .Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
        }

        public void InitializeDatabase()
        {
            using var context = new FinanceContext();
            context.Database.EnsureCreated();
        }

        public SpendingLimit? GetLimitForCurrentMonth()
        {
            using var context = new FinanceContext();
            var now = DateTime.Now;
            return context.SpendingLimits
                .FirstOrDefault(l => l.Month.Year == now.Year && l.Month.Month == now.Month);
        }

        public decimal GetTotalExpensesThisMonth()
        {
            using var context = new FinanceContext();
            var now = DateTime.Now;
            return context.Transactions
                .Where(t => t.Type == Transaction.TransactionType.Expense &&
                            t.Date.Month == now.Month && t.Date.Year == now.Year)
                .Sum(t => t.Amount);
        }

        public bool CanAddExpense(decimal amount)
        {
            var limit = GetLimitForCurrentMonth();
            if (limit == null) return true;

            var spent = GetTotalExpensesThisMonth();
            return spent + amount <= limit.LimitAmount;
        }

    }
}
