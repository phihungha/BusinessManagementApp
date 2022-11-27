using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IProductRepository
    {

        //ProductCategory
        IObservable<List<ProductCategory>> GetProductCategorys();

        IObservable<ProductCategory> GetProductCategory(int id);

        IObservable<ProductCategory> SaveProductCategory(ProductCategory ProductCategory);

        IObservable<ProductCategory> UpdateProductCategory(int ProductCategoryId, ProductCategory request);

        IObservable<Object> DeleteProductCategory(int id);

        //Product
        IObservable<List<Product>> GetProducts();

        IObservable<Product> GetProduct(int id);

        IObservable<Product> SaveProduct(Product Product);

        IObservable<Product> UpdateProduct(int ProductId, Product request);

        IObservable<Object> DeleteProduct(int id);

        //Provider
        IObservable<List<Provider>> GetProviders();

        IObservable<Provider> GetProvider(int id);

        IObservable<Provider> SaveProvider(Provider Provider);

        IObservable<Provider> UpdateProvider(int ProviderId, Provider request);

        IObservable<Object> DeleteProvider(int id);

    }
}
