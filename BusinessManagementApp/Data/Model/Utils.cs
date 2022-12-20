using System.ComponentModel;

namespace BusinessManagementApp.Data.Model
{
    public enum Gender
    {
        [Description("Male")]
        Male,

        [Description("Female")]
        Female,

        [Description("Other")]
        Other
    }
}