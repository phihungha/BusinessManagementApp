using System;

namespace BusinessManagementApp.Data.Model
{
    public class Position : IEquatable<Position>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SupplementSalary { get; set; }

        // Can view and change everything
        public bool CanManageAll { get; set; }

        // Can manage orders (place, edit, cancel orders)
        public bool CanManageOrders { get; set; }

        // Can manage sales-related info (products, providers, sales report)
        public bool CanManageSales { get; set; }

        // Can view sales-related info (products, providers, sales report)
        public bool CanViewSales { get; set; }

        // Can view HR-related info  (salary, overtime, skills, bonus)
        public bool CanViewHr { get; set; }

        // Can manage HR-related  (salary, overtime, skills, bonus)
        public bool CanManageHr { get; set; }

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

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public Position Position { get; set; }
    }
}