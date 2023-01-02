using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IDepartmentsApi
    {
        [Get("/")]
        IObservable<List<Department>> GetDepartments();

        [Get("/{id}")]
        IObservable<Department> GetDepartment(int id);

        [Post("/")]
        IObservable<Department> SaveDepartment([Body] Department department);

        [Patch("/{id}")]
        IObservable<Department> UpdateDepartment(int id, [Body] Department department);

        [Delete("/{id}")]
        IObservable<object> DeleteDepartment(int id);
    }
}