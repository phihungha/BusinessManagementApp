namespace BusinessManagementApp.Data.Model
{
    public class EmployeeStats
    {
        // Returns only Id, Name
        public Employee Employee { get; set; }

        public int NumOfOrders { get; set; }

        public decimal Revenue { get; set; }
    }
}