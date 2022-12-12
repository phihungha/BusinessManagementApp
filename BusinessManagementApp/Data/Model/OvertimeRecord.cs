using System;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {
        public string EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}