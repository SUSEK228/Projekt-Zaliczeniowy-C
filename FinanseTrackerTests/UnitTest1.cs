using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FinanceTrackerTests
{
    [TestClass]
    public class UnitTest1
    {
        // Tworzy konfiguracjê dla bazy danych w pamiêci (InMemory) do celów testowych
        private DbContextOptions<FinanceContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<FinanceContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void AddTransaction()
        {
            // Testuje dodanie nowej transakcji i sprawdza czy zosta³a zapisana w bazie
            var options = GetInMemoryOptions();

            using (var context = new FinanceContext(options))
            {
                var manager = new TransactionManager(context);
                var transaction = new Transaction
                {
                    Description = "Test",
                    Amount = 100,
                    Date = DateTime.Now,
                    Type = Transaction.TransactionType.Income
                };

                manager.AddTransaction(transaction);
            }

            using (var context = new FinanceContext(options))
            {
                Assert.AreEqual(1, context.Transactions.Count());
                Assert.AreEqual("Test", context.Transactions.First().Description);
            }
        }

        [TestMethod]
        public void GetBalance()
        {
            // Testuje poprawnoœæ obliczania salda (przychody - wydatki)
            var options = GetInMemoryOptions();

            using (var context = new FinanceContext(options))
            {
                context.Transactions.AddRange(
                    new Transaction { Description = "Wynagrodzenie", Amount = 1000, Date = DateTime.Now, Type = Transaction.TransactionType.Income },
                    new Transaction { Description = "Zakupy", Amount = 300, Date = DateTime.Now, Type = Transaction.TransactionType.Expense }
                );
                context.SaveChanges();
            }

            using (var context = new FinanceContext(options))
            {
                var manager = new TransactionManager(context);
                Assert.AreEqual(700, manager.GetBalance());
            }
        }

        [TestMethod]
        public void LimitExpenses()
        {
            // Testuje przekroczenie limitu wydatków w bie¿¹cym miesi¹cu
            var options = GetInMemoryOptions();
            var now = DateTime.Now;

            using (var context = new FinanceContext(options))
            {
                context.SpendingLimits.Add(new SpendingLimit
                {
                    Month = new DateTime(now.Year, now.Month, 1),
                    LimitAmount = 100
                });

                context.Transactions.Add(new Transaction
                {
                    Description = "Sklep",
                    Amount = 80,
                    Date = now,
                    Type = Transaction.TransactionType.Expense
                });

                context.SaveChanges();
            }

            using (var context = new FinanceContext(options))
            {
                var manager = new TransactionManager(context);
                Assert.IsFalse(manager.CanAddExpense(30)); 
            }
        }
    }
}
