using System;

namespace BusinessManagementApp.Data.Model
{
    public class Contract
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string CompanyRepresentativeEmployeeId { get; set; }

        public ContractType Type { get; set; }

        public bool IsCurrent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}