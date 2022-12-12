using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class DepartmentsRepo
    {
        public IObservable<List<Department>> GetDepartments()
        {
            var departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "Sales",
                },
                new Department()
                {
                    Id = 2,
                    Name = "Human resources",
                },
                new Department()
                {
                    Id = 3,
                    Name = "IT",
                }
            };

            return Observable.FromAsync(() => Task.FromResult(departments));
        }
    }
}