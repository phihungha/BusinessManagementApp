namespace BusinessManagementApp.Data.Model
{
    public class SalaryRecord
    {
        public Employee Employee { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BaseSalary { get; set; }

        public decimal SupplementSalary { get; set; }

        public decimal BonusSalary { get; set; }

        public decimal TotalSalary { get; set; }
    }
}