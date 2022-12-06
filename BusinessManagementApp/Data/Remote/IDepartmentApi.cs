using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IDepartmentApi
    {
        IObservable<List<Department>> GetDepartments();

        IObservable<Department> GetDepartment(int id);

        IObservable<Department> SaveDepartment(Department Department);

        IObservable<Department> UpdateDepartment(int DepartmentId, Department request);

        IObservable<object> DeleteDepartment(int id);
    }
}
