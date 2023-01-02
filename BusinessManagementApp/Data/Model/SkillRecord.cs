using System;

namespace BusinessManagementApp.Data.Model
{
    public class SkillRecord
    {
        public string EmployeeId { get; set; }

        public SkillType SkillType { get; set; }

        public SkillLevel Level { get; set; }

        public DateTime LastUpdatedTime { get; set; }
    }
}