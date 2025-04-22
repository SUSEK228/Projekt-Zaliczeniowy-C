using System.Collections.Generic;
using System.Linq;
using static FinanceTracker.Models.Transaction;

namespace FinanceTracker.Models
{
    public class TransactionManager
    {
        private List<Transaction> _transactions = new();

        public void AddTransaction(Transaction transaction) // Dodaje transakcje do listy
        {
            _transactions.Add(transaction);
        }

        public IEnumerable<Transaction> GetAllTransactions() { return _transactions; } // Zwraca całą listę transakcji

        public decimal GetBalance() // Oblicza aktualne saldo
        {
            return _transactions.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
        }
    }
}
