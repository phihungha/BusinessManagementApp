using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class DepartmentsRepo
    {
        public IObservable<object> DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Department> GetDepartment(int id)
        {
            var department = new Department()
            {
                Id = 1,
                Name = "Phong tai chinh",
                Head = new Employee() { Id = "1", Name = "Vo Dang Thuan" },
                PhoneNumber = "0123456789"
            };

            return Observable.FromAsync(() => Task.FromResult(department));
        }

        public IObservable<List<Department>> GetDepartments()
        {
            var departments = new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "Phong tai chinh",
                    Head = new Employee() { Id = "1", Name = "Vo Dang Thuan" },
                    PhoneNumber = "0123456789"
                },
                new Department()
                {
                    Id = 2,
                    Name = "Phong ke toan",
                    Head = new Employee() { Id = "2", Name = "Vo Dang Khoa" },
                    PhoneNumber = "0123456788"
                }
        };
            return Observable.FromAsync(() => Task.FromResult(departments));
        }

        public IObservable<Employee> AddDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public IObservable<Department> UpdateDepartment(int departmentId, Department department)
        {
            throw new NotImplementedException();
        }
    }
}