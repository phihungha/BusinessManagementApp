﻿namespace BusinessManagementApp.Data.Model
{
    public class ContractType
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal BaseSalary { get; set; }

        public int Period { get; set; }
    }
}