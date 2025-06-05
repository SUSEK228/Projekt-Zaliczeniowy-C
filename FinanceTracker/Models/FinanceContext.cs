using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models
{
    public class FinanceContext : DbContext
    {
        // Reprezentuje tabelę transakcji w bazie danych
        public DbSet<Transaction> Transactions { get; set; }

        // Reprezentuje tabelę limitów wydatków w bazie danych
        public DbSet<SpendingLimit> SpendingLimits { get; set; }

        //Konstruktor
        public FinanceContext() { }

        // Konstruktor przyjmujący opcje konfiguracyjne 
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }

        // Konfiguracja połączenia z bazą danych SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=finances.db"); 
            }
        }
    }
}