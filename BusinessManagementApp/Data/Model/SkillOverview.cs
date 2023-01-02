using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class SkillOverview
    {
        // Only returns Id, Name
        public Employee Employee { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        // Only returns when getting details
        public List<SkillRecord> Skills { get; set; }
    }
}