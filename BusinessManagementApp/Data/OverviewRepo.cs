using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class OverviewRepo
    {
        public IObservable<Overview> GetOverview()
        {
            var overview = new Overview()
            {
                NumOfPendingOrders = 5,
                TotalStock = 120,
                TodayRevenue = 6_256_530,
                PendingOrders = new List<Order>()
                {
                    new Order()
                    {
                        Id = 1,
                        CreationTime = new DateTime(2022,12,23, 13, 22, 40),
                        Customer = new Customer() { Name = "Le Van A", Phone = "0123456789" },
                        TotalAmount = 100_000
                    },
                    new Order()
                    {
                        Id = 2,
                        CreationTime = new DateTime(2022,12,22, 16, 21, 25),
                        Customer = new Customer() { Name = "Le Van B", Phone = "0123456788" },
                        TotalAmount = 250_000
                    },
                    new Order()
                    {
                        Id = 3,
                        CreationTime = new DateTime(2022,12,21, 11, 12, 15),
                        Customer = new Customer() { Name = "Le Van C", Phone = "0123456787" },
                        TotalAmount = 312_000
                    },
                    new Order()
                    {
                        Id = 4,
                        CreationTime = new DateTime(2022,12,20, 15, 54, 21),
                        Customer = new Customer() { Name = "Le Van D", Phone = "0123456786" },
                        TotalAmount = 345_000
                    },
                    new Order()
                    {
                        Id = 5,
                        CreationTime = new DateTime(2022,12,19, 8, 9, 0),
                        Customer = new Customer() { Name = "Le Van E", Phone = "0123456785" },
                        TotalAmount = 445_000
                    }
                }
            };

            return Observable.FromAsync(() => Task.FromResult(overview)).Delay(new TimeSpan(0, 0, 2));
        }
    }
}