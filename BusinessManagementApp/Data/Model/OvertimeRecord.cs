using System;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {
        public string EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int From { get; set; }

        public int To { get; set; }
    }

    public class OvertimeOverview
    {
        // Only returns Id and Name
        public Employee Employee { get; set; }

        public int NumOfOvertimeDays { get; set; }

        public int AvgOvertimeDuration { get; set; }

        public decimal TotalOvertimePay { get; set; }
    }
}