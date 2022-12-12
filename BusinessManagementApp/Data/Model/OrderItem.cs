namespace BusinessManagementApp.Data.Model
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
    }
}