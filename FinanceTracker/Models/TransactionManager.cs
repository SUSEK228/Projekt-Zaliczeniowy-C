using System.Collections.Generic;
using System.Linq;
using static FinanceTracker.MainWindow;
using static FinanceTracker.Models.Transaction;

namespace FinanceTracker.Models
{
    public class TransactionManager
    {
        private readonly FinanceContext _context;

        // Inicjalizacja managera z kontekstem bazy danych
        public TransactionManager(FinanceContext? context = null)
        {
            _context = context ?? new FinanceContext();
        }

        // Dodawanie nowej transakcji
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        // Usuwanie istniejącej transakcji
        public void DeleteTransaction(Transaction transaction)
        {
            var toRemove = _context.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (toRemove != null)
            {
                _context.Transactions.Remove(toRemove);
                _context.SaveChanges();
            }
        }

        // Pobranie wszystkich transakcji z bazy
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        // Obliczanie aktualnego salda
        public decimal GetBalance()
        {
            return _context.Transactions
               .Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
        }

        // Tworzenie bazy danych jeśli nie istnieje
        public void InitializeDatabase()
        {
            _context.Database.EnsureCreated();
        }

        // Pobieranie limitu wydatków na bieżący miesiąc
        public SpendingLimit? GetLimitForCurrentMonth()
        {
            var now = DateTime.Now;
            return _context.SpendingLimits
                .FirstOrDefault(l => l.Month.Year == now.Year && l.Month.Month == now.Month);
        }

        // Obliczanie sumy wydatków w bieżącym miesiącu
        public decimal GetTotalExpensesThisMonth()
        {
            var now = DateTime.Now;
            return _context.Transactions
                .Where(t => t.Type == Transaction.TransactionType.Expense &&
                            t.Date.Month == now.Month && t.Date.Year == now.Year)
                .Sum(t => t.Amount);
        }

        // Sprawdzenie czy dodanie nowego wydatku nie przekroczy limitu
        public bool CanAddExpense(decimal amount)
        {
            var limit = GetLimitForCurrentMonth();
            if (limit == null) return true;

            var spent = GetTotalExpensesThisMonth();
            return spent + amount <= limit.LimitAmount;
        }
    }
}
