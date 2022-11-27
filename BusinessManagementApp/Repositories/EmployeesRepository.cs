using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Repositories
{
    public class EmployeesRepository
    {
        public async Task<List<Employee>> GetAllEmployees()
        {
            await Task.Delay(1000);

            return new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name = "Nguyen Van A",
                    Gender = "Male",
                    BirthDate = new DateTime(1999, 3, 5),
                    Position = "IT manager",
                    Department = "IT"
                },

                new Employee()
                {
                    Id = 2,
                    Name = "Nguyen Van B",
                    Gender = "Female",
                    BirthDate = new DateTime(1986, 12, 5),
                    Position = "HR manager",
                    Department = "Human resources"
                },

                new Employee()
                {
                    Id = 3,
                    Name = "Nguyen Van C",
                    Gender = "Male",
                    BirthDate = new DateTime(1994, 4, 12),
                    Position = "Sales",
                    Department = "Sales"
                }
            };
        }
    }
}
