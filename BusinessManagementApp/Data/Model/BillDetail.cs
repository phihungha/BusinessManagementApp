namespace BusinessManagementApp.Data.Model
{
    public class OrderItem
    {
        public int BillId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }
    }
}