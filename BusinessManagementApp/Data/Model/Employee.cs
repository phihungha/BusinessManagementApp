using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Employee
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string CitizenId { get; set; }

        public DateTime BirthDate { get; set; }

        public string Education { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public List<PositionRecord> PositionRecords { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Contract> Contracts { get; set; }

        public DateTime? TerminationDate { get; set; }

        public Department Department { get; set; }
    }
}