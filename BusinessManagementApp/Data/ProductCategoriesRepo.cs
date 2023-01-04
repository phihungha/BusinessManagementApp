using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ProductCategoriesRepo
    {
        public IObservable<object> DeleteProductCategory(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<ProductCategory> GetProductCategory(int id)
        {
            var productCategory = new ProductCategory()
            {
                Id = 1,
                Name = "may tinh",
                Description = "hang de vo"
            };
            return Observable.FromAsync(() => Task.FromResult(productCategory));
        }

        public IObservable<List<ProductCategory>> GetProductCategories()
        {
            var productCategories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Id = 1,
                    Name = "may tinh",
                    Description = "hang de vo"
                },
                new ProductCategory()
                {
                    Id = 2,
                    Name = "ram",
                    Description = "hang de vo"
                },
            };
            return Observable.FromAsync(() => Task.FromResult(productCategories));
        }

        public IObservable<SkillType> AddProductCategory(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public IObservable<ProductCategory> UpdateProductCategory(int productCategoryId, ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }
    }
}