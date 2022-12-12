using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class EmployeeRepo
    {
        public EmployeeRepo()
        {
        }

        public IObservable<object> DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> GetEmployee(string id)
        {
            var employee = new Employee()
            {
                Id = id,
                Name = "Nguyen Van A",
                Gender = "Male",
                CitizenId = "123456789000",
                BirthDate = new DateTime(1999, 3, 5),
                Position = "IT manager",
                Department = "IT",
                PhoneNumber = "0123456789",
                Email = "vana@gmail.com",
                Address = "118 Le Hong Phong",
                Qualification = "University"
            };

            return Observable.FromAsync(() => Task.FromResult(employee));
        }

        public IObservable<List<Employee>> GetEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = "1",
                    Name = "Nguyen Van A",
                    Gender = "Male",
                    BirthDate = new DateTime(1999, 3, 5),
                    Position = "IT manager",
                    Department = "IT"
                },

                new Employee()
                {
                    Id = "2",
                    Name = "Nguyen Van B",
                    Gender = "Female",
                    BirthDate = new DateTime(1986, 12, 5),
                    Position = "HR manager",
                    Department = "Human resources"
                },

                new Employee()
                {
                    Id = "3",
                    Name = "Nguyen Van C",
                    Gender = "Male",
                    BirthDate = new DateTime(1994, 4, 12),
                    Position = "Sales",
                    Department = "Sales"
                }
            };
            return Observable.FromAsync(() => Task.FromResult(employees));
        }

        public IObservable<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> UpdateEmployee(string employeeId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}