using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IProductApi
    {

        //ProductCategory
        [Get("/")]
        IObservable<List<ProductCategory>> GetProductCategorys();

        [Get("/{id}")]
        IObservable<ProductCategory> GetProductCategory(int id);

        [Post("/")]
        IObservable<ProductCategory> SaveProductCategory(ProductCategory ProductCategory);

        [Patch("/{id}")]
        IObservable<ProductCategory> UpdateProductCategory(int ProductCategoryId, ProductCategory request);

        [Delete("/{id}")]
        IObservable<object> DeleteProductCategory(int id);

        //Product
        [Get("/")]
        IObservable<List<Product>> GetProducts();

        [Get("/{id}")]
        IObservable<Product> GetProduct(int id);

        [Post("/")]
        IObservable<Product> SaveProduct(Product Product);

        [Patch("/{id}")]
        IObservable<Product> UpdateProduct(int ProductId, Product request);

        [Delete("/{id}")]
        IObservable<object> DeleteProduct(int id);

        //Provider
        [Get("/")]
        IObservable<List<Provider>> GetProviders();

        [Get("/{id}")]
        IObservable<Provider> GetProvider(int id);

        [Post("/")]
        IObservable<Provider> SaveProvider(Provider Provider);

        [Patch("/{id}")]
        IObservable<Provider> UpdateProvider(int ProviderId, Provider request);

        [Delete("/{id}")]
        IObservable<object> DeleteProvider(int id);

    }
}
