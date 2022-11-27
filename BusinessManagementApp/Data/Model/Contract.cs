using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class Contract
    {

        public int Id { get; set; }

        public String EmployeeId { get; set; }
 
        public String Name { get; set; }

        public String Description { get; set; }

        public ContractType Type { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public decimal Salary { get; set; }

    }
}
