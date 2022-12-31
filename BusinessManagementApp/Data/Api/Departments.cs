using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public partial interface IApiClient
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
