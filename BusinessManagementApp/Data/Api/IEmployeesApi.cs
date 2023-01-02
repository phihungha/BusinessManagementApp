using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IEmployeesApi
    {
        [Get("/")]
        IObservable<List<Employee>> GetEmployees();

        [Get("/{id}")]
        IObservable<Employee> GetEmployee(string id);

        [Post("/")]
        IObservable<Employee> SaveEmployee([Body] Employee employee);

        [Patch("/{id}")]
        IObservable<Employee> UpdateEmployee(string id, [Body] Employee employee);

        [Delete("/{id}")]
        IObservable<object> DeleteEmployee(string id);

        /// <summary>
        /// TODO: Get all contracts of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>List of contracts</returns>
        [Get("/{employeeId}/contracts")]
        IObservable<List<Contract>> GetContracts(string employeeId);

        /// <summary>
        /// TODO: Create a future contract for an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="contract">Future contract</param>
        /// <returns>Future contract</returns>
        [Post("/{employeeId}/contracts")]
        IObservable<Contract> SaveFutureContract(string employeeId, [Body] Contract contract);

        /// <summary>
        /// TODO: Update the future contract of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="contract">Future contract</param>
        /// <returns>Future contract</returns>
        [Patch("/{employeeId}/contracts")]
        IObservable<Contract> UpdateFutureContract(string employeeId, [Body] Contract contract);

        /// <summary>
        /// TODO: Delete the future contract of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        [Delete("/{employeeId}/contracts")]
        IObservable<object> DeleteFutureContract(string employeeId);
    }
}