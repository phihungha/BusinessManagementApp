using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BusinessManagementApp.Data.Model
{
    public static class StringUtils
    {
        public static string toLowerCamelCase(string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string toUndercore(string str)
        {
            return Regex.Replace(str, "([A-Z])", "_$1", RegexOptions.Compiled).TrimStart('_').ToLower();
        }
    }

    public enum Gender
    {
        [Description("Male")] Male,

        [Description("Female")] Female,

        [Description("Other")] Other
    }
}