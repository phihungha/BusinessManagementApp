using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class Position : IEquatable<Position>
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("supplement_salary")] public decimal SupplementSalary { get; set; }

        // Can view orders
        [JsonProperty("can_view_orders")] public bool CanViewOrders { get; set; }

        // Can manage orders
        [JsonProperty("can_manage_orders")] public bool CanManageOrders { get; set; }

        // Can view customers
        [JsonProperty("can_view_customers")] public bool CanViewCustomers { get; set; }

        // Can manage customers
        [JsonProperty("can_manage_customers")] public bool CanManageCustomers { get; set; }

        // Can view sales info  (products, providers, vouchers, sales report)
        [JsonProperty("can_view_sales")] public bool CanViewSales { get; set; }

        // Can manage sales info (products, providers, vouchers, sales report)
        [JsonProperty("can_manage_sales")] public bool CanManageSales { get; set; }

        // Can view human resources (salary, overtime, skill ratings, bonuses)
        [JsonProperty("can_view_hr")] public bool CanViewHr { get; set; }

        // Can manage human resources (salary, overtime, skill ratings, bonuses)
        [JsonProperty("can_manage_hr")] public bool CanManageHr { get; set; }

        // Can view configuration  (departments, product categories, positions, contract types,...)
        [JsonProperty("can_view_config")] public bool CanViewConfig { get; set; }

        // Can manage configuration  (departments, product categories, positions, contract types,...)
        [JsonProperty("can_manage_config")] public bool CanManageConfig { get; set; }

        public bool Equals(Position? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}