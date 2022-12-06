using System;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {
        public string EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}