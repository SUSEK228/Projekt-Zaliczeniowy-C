using System;

namespace FinanceTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; } // *Nowe* Dodajemy klucz głowny
        public required DateTime Date { get; set; } // Data transakcji
        public required string Description { get; set; } // Opis np. Zakupy, Wypłata
        public required decimal Amount { get; set; } // Kwota transakcji
        public required TransactionType Type { get; set; }

        public enum TransactionType
        {
            Income, // Przychód 
            Expense // Wydatek
        }
    }
}
