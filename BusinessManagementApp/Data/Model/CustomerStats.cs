namespace BusinessManagementApp.Data.Model
{
    public class CustomerStats
    {
        public Customer Customer { get; set; }

        public int NumOfOrders { get; set; }

        public decimal Revenue { get; set; }
    }
}