using System;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {
        public string EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int NumOfHours { get; set; }
    }
}