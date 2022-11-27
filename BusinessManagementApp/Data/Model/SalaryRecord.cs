using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class SalaryRecord
    {

        public String EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BasicSalary { get; set; }

        public byte BonusType { get; set; }

        public decimal Bonus { get; set; }

    }
}
