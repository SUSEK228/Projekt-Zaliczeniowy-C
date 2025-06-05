using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Models
{
    public class SpendingLimit
    {
        public int Id { get; set; } // Unikalny identyfikator limitu

        public decimal LimitAmount { get; set; } // Kwota limitu wydatków

        public DateTime Month { get; set; } // Miesiąc, którego dotyczy limit
    }

}

