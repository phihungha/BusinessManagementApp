using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeOverview
    {
        public DateTime MonthYear { get; set; }

        // Only returns Id and Name
        public Employee Employee { get; set; }

        public int NumOfOvertimeDays { get; set; }

        public double AvgOvertimeDuration { get; set; }

        public decimal TotalOvertimePay { get; set; }

        // Only returns when getting details
        public List<OvertimeRecord> Records { get; set; }
    }
}