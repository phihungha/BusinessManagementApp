using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {

        public String EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}
