using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
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
