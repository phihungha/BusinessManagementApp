using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Position : IEquatable<Position>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SupplementSalary { get; set; }

        public List<Permission> Permissions { get; set; }

        public bool Equals(Position? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }

    public class PositionRecord
    {
        public string EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Position Position { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte ValueType { get; set; }

        public string StringValue { get; set; }

        public decimal IntValue { get; set; }

        public double DoubleValue { get; set; }

        public bool BooleanValue { get; set; }
    }
}