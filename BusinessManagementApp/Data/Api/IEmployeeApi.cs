using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public partial interface IApiClient
    {
        [Get("/")]
        IObservable<List<Employee>> GetEmployees();

        [Get("/{id}")]
        IObservable<Employee> GetEmployee(string id);

        [Post("/")]
        IObservable<Employee> SaveEmployee(Employee Employee);

        [Patch("/{id}")]
        IObservable<Employee> UpdateEmployee(string id, Employee request);

        [Delete("/{id}")]
        IObservable<object> DeleteEmployee(string id);
    }
}
