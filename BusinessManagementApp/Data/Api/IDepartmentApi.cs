using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Api
{
    public interface IDepartmentApi
    {
        [Get("/")]
        IObservable<List<Department>> GetDepartments();

        [Get("/{id}")]
        IObservable<Department> GetDepartment(int id);

        [Post("/")]
        IObservable<Department> SaveDepartment(Department Department);

        [Patch("/{id}")]
        IObservable<Department> UpdateDepartment(int DepartmentId, Department request);

        [Delete("/{id}")]
        IObservable<object> DeleteDepartment(int id);
    }
}
