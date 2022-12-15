using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ProductsRepo
    {
        public IObservable<object> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<List<ProductCategory>> GetCategories()
        {
            var categories = new List<ProductCategory>()
            {
                new ProductCategory()
                    {
                        Id=1,
                        Name = "phone",
                        Description= "abc",
                    },
                new ProductCategory()
                    {
                        Id=2,
                        Name = "Laptop",
                        Description= "abc",
                    }
            };
            return Observable.FromAsync(() => Task.FromResult(categories));
        }

        public IObservable<Product> GetProduct(int id)
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Iphone",
                Description = "abc",
                Unit = "abc",
                Stock = 10,
                Price = 1000,
                Category = new ProductCategory()
                    {
                        Id=1,
                        Name = "phone",
                        Description= "abc",
                    }
            };

            return Observable.FromAsync(() => Task.FromResult(product));
        }

        public IObservable<List<Product>> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Iphone",
                    Description = "abc",
                    Unit = "abc",
                    Stock = 10,
                    Price = 1000,
                    Category = new ProductCategory()
                    {
                        Id=1,
                        Name = "phone",
                        Description= "abc",
                    }
                },
                new Product()
                {
                    Id = 2,
                    Name = "XPS15",
                    Description = "abc",
                    Unit = "abc",
                    Stock = 10,
                    Price = 1000,
                    Category = new ProductCategory()
                    {
                        Id=2,
                        Name = "Laptop",
                        Description= "abc",
                    }
                }
            };
            return Observable.FromAsync(() => Task.FromResult(products));
        }

        public IObservable<Employee> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
