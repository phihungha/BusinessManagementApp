using System;

namespace BusinessManagementApp.Data.Model
{
    public class Employee
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = "";

        public string Gender { get; set; } = "";

        public DateTime BirthDate { get; set; } = new();

        public string Position { get; set; } = "";

        public string Department { get; set; } = "";
    }
}