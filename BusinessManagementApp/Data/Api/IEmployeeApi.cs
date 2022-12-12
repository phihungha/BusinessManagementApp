﻿using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Api
{
    public interface IEmployeeApi
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
