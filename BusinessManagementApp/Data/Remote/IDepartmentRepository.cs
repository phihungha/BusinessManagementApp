using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IDepartmentRepository
    {
        IObservable<List<Department>> GetDepartments();

        IObservable<Department> GetDepartment(int id);

        IObservable<Department> SaveDepartment(Department Department);

        IObservable<Department> UpdateDepartment(int DepartmentId, Department request);

        IObservable<Object> DeleteDepartment(int id);
    }
}
