namespace BusinessManagementApp.Data.Model
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public Product Product { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}