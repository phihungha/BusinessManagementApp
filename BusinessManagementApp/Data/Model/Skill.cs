using System;

namespace BusinessManagementApp.Data.Model
{
    public class Skill
    {
        public string EmployeeId { get; set; } = "";

        public string SkillId { get; set; } = "";

        public int Level { get; set; } = 0;

        public DateTime UpdatedAt { get; set; } = new();
    }
}