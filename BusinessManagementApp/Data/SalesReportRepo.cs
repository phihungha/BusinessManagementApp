using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class SalesReportRepo
    {
        public IObservable<SalesStats> GetSalesReport(int year, int month)
        {
            var report = new SalesStats()
            {
                RevenueByDay = { 300_000, 500_000, 150_000 },
                TotalRevenue = 1_000_000,
                AvgRevenue = 750_000,
                AvgRevenuePerEmployee = 550_000,
                NumOfOrdersCompleted = 5,
                NumOfOrdersReturned = 1,
                NumOfOrdersCanceled = 2,
                NumOfOrdersMade = 8,
                ProductCategoryStats = new List<ProductCategoryStats>
                {
                    new ProductCategoryStats()
                    {
                        Category = new ProductCategory() { Id = 1, Name = "CPU"},
                        QuantitySold = 10
                    },
                    new ProductCategoryStats()
                    {
                        Category = new ProductCategory() { Id = 2, Name = "GPU"},
                        QuantitySold = 5
                    },
                },
                ProductStats = new List<ProductStats>
                {
                    new ProductStats()
                    {
                        Product = new Product() { Id = 1, Name = "CPU 1"},
                        QuantitySold = 10,
                        Revenue = 1_000_000
                    },
                    new ProductStats()
                    {
                        Product = new Product() { Id = 2, Name = "CPU 2"},
                        QuantitySold = 5,
                        Revenue = 500_000
                    }
                },
                EmployeeStats = new List<EmployeeStats>
                {
                    new EmployeeStats()
                    {
                        Employee = new Employee() { Id = "1", Name = "Nguyen Van A"},
                        NumOfOrders = 5,
                        Revenue = 1_000_000
                    },
                    new EmployeeStats()
                    {
                        Employee = new Employee() { Id = "2", Name = "Nguyen Van B"},
                        NumOfOrders = 3,
                        Revenue = 500_000
                    }
                },
                CustomerStats = new List<CustomerStats>
                {
                    new CustomerStats()
                    {
                        Customer = new Customer() { Id = "1", Name = "Nguyen Van A"},
                        NumOfOrders = 5,
                        Revenue = 1_000_000
                    },
                    new CustomerStats()
                    {
                        Customer = new Customer() { Id = "2", Name = "Nguyen Van B"},
                        NumOfOrders = 3,
                        Revenue = 500_000
                    }
                }
            };

            return Observable.FromAsync(() => Task.FromResult(report));
        }
    }
}