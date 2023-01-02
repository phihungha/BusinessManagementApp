using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IProductsApi
    {
        [Get("/")]
        IObservable<List<Product>> GetProducts();

        [Get("/{id}")]
        IObservable<Product> GetProduct(int id);

        [Post("/")]
        IObservable<Product> SaveProduct([Body] Product product);

        [Patch("/{id}")]
        IObservable<Product> UpdateProduct(int id, [Body] Product product);

        [Delete("/{id}")]
        IObservable<object> DeleteProduct(int id);
    }
}