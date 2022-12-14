using System;

namespace BusinessManagementApp.Data.Model
{
    public class Contract
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public Employee CompanyRepresentative { get; set; }

        public ContractType Type { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }

    public class ContractType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal BaseSalary { get; set; }

        public int? Period { get; set; }
    }
}