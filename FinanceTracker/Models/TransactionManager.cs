using System.Collections.Generic;
using System.Linq;
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
    }
}
