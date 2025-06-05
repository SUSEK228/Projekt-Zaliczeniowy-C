using System;

namespace FinanceTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; } // Identyfikator transakcji (klucz główny)

        public required DateTime Date { get; set; } // Data transakcji

        public required string Description { get; set; } // Opis transakcji

        public required decimal Amount { get; set; } // Kwota

        public required TransactionType Type { get; set; } // Typ transakcji: przychód lub wydatek

        public enum TransactionType
        {
            Income,  
            Expense  
        }
    }

}
