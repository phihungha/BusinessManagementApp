using System;

namespace BusinessManagementApp.Data.Model
{
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