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

        public int[] Categories { get; set; }
    }
}