using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class SalaryRecordsRepo
    {      
        public IObservable<List<SalaryRecord>> GetSalaryRecords()
        {
            var salaryrecords = new List<SalaryRecord>()
            {
                new SalaryRecord()
                {
                    Employee = new Employee()
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
                        CurrentPosition = new Position() { Name = "Sales manager" },
                    },
                    BaseSalary = 1000,
                    SupplementSalary = 2000,
                    BonusSalary = 1000,
                    Month = 3,
                    Year = 2023,
                    TotalSalary = 4000
                },

                new SalaryRecord()
                {
                    Employee = new Employee()
                    {
                        Id = "3",
                        Name = "Mai Thi",
                        Gender = Gender.Female,
                        CitizenId = "123456789009",
                        BirthDate = new DateTime(1975, 5, 5),
                        PhoneNumber = "0123456780",
                        Department = new Department { Id = 2, Name = "Solution" },
                        CurrentPosition = new Position() { Name = "manager" }
                    },
                    BaseSalary = 1000,
                    SupplementSalary = 1000,
                    BonusSalary = 1000,
                    Month = 3,
                    Year = 2023,
                    TotalSalary = 3000
                },

                new SalaryRecord()
                {
                    Employee = new Employee()
                    {
                        Id = "3",
                        Name = "Mai Thi",
                        Gender = Gender.Female,
                        CitizenId = "123456789009",
                        BirthDate = new DateTime(1975, 5, 5),
                        PhoneNumber = "0123456780",
                        Department = new Department { Id = 2, Name = "Human resources" },
                        CurrentPosition = new Position() { Name = "HR manager" }
                    },
                    BaseSalary = 1000,
                    SupplementSalary = 1000,
                    BonusSalary = 1000,
                    Month = 1,
                    Year = 2023,
                    TotalSalary = 3000
                },

               new SalaryRecord()
                {
                    Employee = new Employee()
                    {
                        Id = "2",
                        Name = "Mai Thi Xuan",
                        Gender = Gender.Female,
                        CitizenId = "123456789001",
                        BirthDate = new DateTime(1975, 5, 5),
                        PhoneNumber = "0123456780",
                        Department = new Department { Id = 2, Name = "Human resources" },
                        CurrentPosition = new Position() { Name = "HR manager" }
                    },
                    BaseSalary = 1000,
                    SupplementSalary = 1000,
                    BonusSalary = 1000,
                    Month = 3,
                    Year = 2023,
                    TotalSalary = 3000
                },

            };
            return Observable.FromAsync(() => Task.FromResult(salaryrecords));
        }

    }
}
