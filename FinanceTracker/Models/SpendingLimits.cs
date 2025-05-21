using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Models
{
    public class SpendingLimit
    {
        public int Id { get; set; }
        public decimal LimitAmount { get; set; }
        public DateTime Month { get; set; }
    }
}

