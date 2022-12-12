namespace BusinessManagementApp.Data.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public Employee Head { get; set; }
    }
}