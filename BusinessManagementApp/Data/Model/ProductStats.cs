namespace BusinessManagementApp.Data.Model
{
    public class ProductStats
    {
        // Returns only Id, Name
        public Product Product { get; set; }

        public int QuantitySold { get; set; }

        public decimal Revenue { get; set; }
    }
}