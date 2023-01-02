using System;

namespace BusinessManagementApp.Data.Model
{
    public class BonusRecord
    {
        public DateTime MonthYear { get; set; }

        public Employee Employee { get; set; }

        public BonusType Type { get; set; }

        public decimal Amount { get; set; }
    }
}