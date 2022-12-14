using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class EmployeeRepo
    {
        public IObservable<object> DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Employee> GetEmployee(string id)
        {
            var employee = new Employee()
            {
                Id = "1",
                Name = "Nguyen Van A",
                Gender = Gender.Male,
                CitizenId = "123456789000",
                BirthDate = new DateTime(1975, 5, 5),
                Education = "Bach Khoa Universitys",
                PhoneNumber = "0123456789",
                Email = "NguyenA@gmail.com",
                Address = "178 Nguyen Trai, Binh Duong",
                Department = new Department { Id = 2, Name = "Human resources" },
                CurrentPosition = new Position() { Id = 4, Name = "Director" },
                PositionRecords = new List<PositionRecord>
                {
                    new PositionRecord()
                    {
                        StartDate = new DateTime(2020, 5, 5),
                        IsCurrent = true,
                        Position = new Position { Name = "Director" }
                    },

                    new PositionRecord()
                    {
                        StartDate = new DateTime(2015, 5, 5),
                        EndDate = new DateTime(2020, 5, 5),
                        IsCurrent = false,
                        Position = new Position { Name = "Sales manager" }
                    },

                    new PositionRecord()
                    {
                        StartDate = new DateTime(2011, 5, 5),
                        EndDate = new DateTime(2015, 5, 5),
                        IsCurrent = false,
                        Position = new Position { Name = "Sales" }
                    }
                },
                Contracts = new List<Contract>
                {
                    new Contract()
                    {
                        Id = 1,
                        StartDate = new DateTime(2021, 5, 5),
                        IsCurrent = true,
                        Type = new ContractType() { Name = "Permanent" }
                    },

                    new Contract()
                    {
                        Id = 2,
                        StartDate = new DateTime(2011, 5, 5),
                        EndDate = new DateTime(2021,5,5),
                        IsCurrent = false,
                        Type = new ContractType() { Name = "10 years" }
                    }
                },
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
                    Gender = Gender.Male,
                    CitizenId = "123456789000",
                    BirthDate = new DateTime(1975, 5, 5),
                    PhoneNumber = "0123456789",
                    Department = new Department { Id = 1, Name = "Sales" },
                    CurrentPosition = new Position() { Name = "Sales manager" }
                },

                new Employee()
                {
                    Id = "2",
                    Name = "Mai Thi Xuan",
                    Gender = Gender.Female,
                    CitizenId = "123456789001",
                    BirthDate = new DateTime(1975, 5, 5),
                    PhoneNumber = "0123456780",
                    Department = new Department { Id = 2, Name = "Human resources" },
                    CurrentPosition = new Position() { Name = "HR manager" }
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