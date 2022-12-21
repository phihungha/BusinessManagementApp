using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessManagementApp.Data.Model
{
    public class SkillOverview
    {
        // Only returns Id, Name
        public Employee Employee { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Only returns when getting details
        public List<Skill> Skills { get; set; }
    }

    public class Skill
    {
        public string EmployeeId { get; set; }

        public SkillType SkillType { get; set; }

        public SkillLevel Level { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

    public class SkillType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public enum SkillLevel
    {
        [Description("Acceptable")]
        Acceptable,

        [Description("Good")]
        Good,

        [Description("Excellent")]
        Excellent
    }
}