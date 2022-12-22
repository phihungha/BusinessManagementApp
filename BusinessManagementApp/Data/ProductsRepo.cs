using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ProductsRepo
    {
        public IObservable<List<Product>> GetAvailableProducts()
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "INTEL Core i3-10105",
                    Description = "4C/8T, 3.7GHz - 4.4GHz, 6MB",
                    Unit = "",
                    Price = 2_899_000,
                    Stock = 100,
                    Category = new ProductCategory () { Id = 1, Name = "CPU"}
                },
                new Product()
                {
                    Id = 2,
                    Name = "INTEL Core i5-12400",
                    Description = "6C/12T, 2.50 GHz - 4.40 GHz, 18MB",
                    Unit = "",
                    Price = 5_049_000,
                    Stock = 50,
                    Category = new ProductCategory () { Id = 1, Name = "CPU"}
                },
                new Product()
                {
                    Id = 3,
                    Name = "ASUS TUF Gaming GeForce GTX 1660 SUPER OC Edition",
                    Description = "6GB GDDR6 TUF-GTX1660S-O6G-GAMING",
                    Unit = "",
                    Price = 4_490_000,
                    Stock = 1,
                    Category = new ProductCategory () { Id = 1, Name = "Graphics Card"}
                }
            };

            return Observable.FromAsync(() => Task.FromResult(products));
        }
    }
}