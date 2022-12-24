using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class SalesStats
    {
        public List<decimal> RevenueByDay { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal AvgRevenue { get; set; }

        public double AvgNumOfOrdersPerEmployee { get; set; }

        public int NumOfOrdersCompleted { get; set; }

        public int NumOfOrdersCanceled { get; set; }

        public int NumOfOrdersReturned { get; set; }

        public int NumOfOrdersMade { get; set; }

        public List<ProductCategoryStats> ProductCategoryStats { get; set; }

        public List<ProductStats> ProductStats { get; set; }

        public List<CustomerStats> CustomerStats { get; set; }

        public List<EmployeeStats> EmployeeStats { get; set; }
    }

    public class ProductCategoryStats
    {
        public ProductCategory Category { get; set; }

        public int QuantitySold { get; set; }
    }

    public class ProductStats
    {
        // Returns only Id, Name
        public Product Product { get; set; }

        public int QuantitySold { get; set; }

        public decimal Revenue { get; set; }
    }

    public class CustomerStats
    {
        public Customer Customer { get; set; }

        public int NumOfOrders { get; set; }

        public decimal Revenue { get; set; }
    }

    public class EmployeeStats
    {
        // Returns only Id, Name
        public Employee Employee { get; set; }

        public int NumOfOrders { get; set; }

        public decimal Revenue { get; set; }
    }
}