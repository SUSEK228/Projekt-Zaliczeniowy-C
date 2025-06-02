using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Models
{
    public class FinanceContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SpendingLimit> SpendingLimits { get; set; }

        public FinanceContext() { }

        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=finances.db");
            }
        }
    }
}