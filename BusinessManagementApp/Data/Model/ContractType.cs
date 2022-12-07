using System;

namespace BusinessManagementApp.Data.Model
{
    public class ContractType
    {

        public String Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public decimal BaseSalary { get; set; }

        public int Period { get; set; }

    }
}