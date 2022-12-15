using System;

namespace BusinessManagementApp.Data.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public ProductCategory Category { get; set; }
    }

    public class ProductCategory : IEquatable<ProductCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Equals(ProductCategory? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}