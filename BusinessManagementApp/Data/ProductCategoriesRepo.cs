using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class ProductCategoriesRepo
    {
        private IProductCategoriesApi api;

        public ProductCategoriesRepo(IProductCategoriesApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeleteProductCategory(int id)
        {
            return api.DeleteProductCategory(id);
        }

        public IObservable<ProductCategory> GetProductCategory(int id)
        {
            return api.GetProductCategory(id);
        }

        public IObservable<List<ProductCategory>> GetProductCategories()
        {
            return api.GetProductCategories();
        }

        public IObservable<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            return api.SaveProductCategory(productCategory);
        }

        public IObservable<ProductCategory> UpdateProductCategory(int productCategoryId, ProductCategory productCategory)
        {
            return api.UpdateProductCategory(productCategoryId, productCategory);
        }
    }
}