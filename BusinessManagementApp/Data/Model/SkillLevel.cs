using System.ComponentModel;

namespace BusinessManagementApp.Data.Model
{
    public enum SkillLevel
    {
        [Description("Unrated")]
        Unrated,

        [Description("Acceptable")]
        Acceptable,

        [Description("Good")]
        Good,

        [Description("Excellent")]
        Excellent
    }
}