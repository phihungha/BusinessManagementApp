namespace BusinessManagementApp.Data.Model
{
    public class SalaryRecord
    {
        public string EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BasicSalary { get; set; }

        public byte BonusType { get; set; }

        public decimal Bonus { get; set; }
    }
}