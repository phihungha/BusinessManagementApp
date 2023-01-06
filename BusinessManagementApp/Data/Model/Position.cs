using System;

namespace BusinessManagementApp.Data.Model
{
    public class Position : IEquatable<Position>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SupplementSalary { get; set; }

        // Can view orders
        public bool CanViewOrders { get; set; }

        // Can manage orders
        public bool CanManageOrders { get; set; }

        // Can view customers
        public bool CanViewCustomers { get; set; }

        // Can manage customers
        public bool CanManageCustomers { get; set; }

        // Can view sales info  (products, providers, vouchers, sales report)
        public bool CanViewSales { get; set; }

        // Can manage sales info (products, providers, vouchers, sales report)
        public bool CanManageSales { get; set; }

        // Can view human resources (salary, overtime, skill ratings, bonuses)
        public bool CanViewHr { get; set; }

        // Can manage human resources (salary, overtime, skill ratings, bonuses)
        public bool CanManageHr { get; set; }

        // Can view configuration  (departments, product categories, positions, contract types,...)
        public bool CanViewConfig { get; set; }

        // Can manage configuration  (departments, product categories, positions, contract types,...)
        public bool CanManageConfig { get; set; }

        public bool Equals(Position? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}