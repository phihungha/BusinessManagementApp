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
}