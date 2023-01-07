using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class ProductsRepo
    {
        private IProductsApi api;
        private IProductCategoriesApi categoriesApi;

        public ProductsRepo(IProductsApi api, IProductCategoriesApi categoriesApi)
        {
            this.api = api;
            this.categoriesApi = categoriesApi;
        }

        public IObservable<object> DeleteProduct(int id)
        {
            return api.DeleteProduct(id);
        }

        public IObservable<List<ProductCategory>> GetCategories()
        {
            return categoriesApi.GetProductCategories();
        }

        public IObservable<Product> GetProduct(int id)
        {
            return api.GetProduct(id);
        }

        public IObservable<List<Product>> GetProducts()
        {
            return api.GetProducts();
        }

        public IObservable<List<Product>> GetAvailableProducts()
        {
            return GetProducts();
        }

        public IObservable<Product> AddProduct(Product product)
        {
            return api.SaveProduct(product);
        }

        public IObservable<Product> UpdateProduct(int productId, Product product)
        {
            return api.UpdateProduct(productId, product);
        }
    }
}