﻿using System;

namespace BusinessManagementApp.Data.Model
{
    public class BonusRecord
    {
        public DateTime MonthYear { get; set; }

        public Employee Employee { get; set; }

        public BonusType Type { get; set; }

        public decimal Amount { get; set; }
    }

    public class BonusType : IEquatable<BonusType>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public bool Equals(BonusType? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}