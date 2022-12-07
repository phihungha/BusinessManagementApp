using System;

namespace BusinessManagementApp.Data.Model
{
    public class PositionRecord
    {
        public string EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PositionId { get; set; }
    }
}