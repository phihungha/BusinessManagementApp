using System;

namespace BusinessManagementApp.Data.Model
{
    public class Contract
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ContractType Type { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }
    }
}