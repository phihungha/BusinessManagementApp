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
}