using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IProductCategoriesApi
    {
        [Get("/")]
        IObservable<List<ProductCategory>> GetProductCategories();

        [Get("/{id}")]
        IObservable<ProductCategory> GetProductCategory(int id);

        [Post("/")]
        IObservable<ProductCategory> SaveProductCategory([Body] ProductCategory productCategory);

        [Patch("/{id}")]
        IObservable<ProductCategory> UpdateProductCategory(int id, [Body] ProductCategory productCategory);

        [Delete("/{id}")]
        IObservable<object> DeleteProductCategory(int id);
    }

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

    public interface IProvidersApi
    {
        [Get("/")]
        IObservable<List<Provider>> GetProviders();

        [Get("/{id}")]
        IObservable<Provider> GetProvider(int id);

        [Post("/")]
        IObservable<Provider> SaveProvider([Body] Provider provider);

        [Patch("/{id}")]
        IObservable<Provider> UpdateProvider(int id, [Body] Provider provider);

        [Delete("/{id}")]
        IObservable<object> DeleteProvider(int id);
    }
}