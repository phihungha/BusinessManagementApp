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
        IObservable<Employee> GetEmployee(int id);

        [Post("/")]
        IObservable<Employee> SaveEmployee(Employee Employee);

        [Patch("/{id}")]
        IObservable<Employee> UpdateEmployee(int EmployeeId, Employee request);

        [Delete("/{id}")]
        IObservable<object> DeleteEmployee(int id);
    }
}
