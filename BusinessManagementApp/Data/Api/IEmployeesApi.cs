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
        /// Get all contracts of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>List of contracts</returns>
        [Get("/{employeeId}/contracts")]
        IObservable<List<Contract>> GetContracts(string employeeId);

        /// <summary>
        /// Terminate current contract.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>List of contracts</returns>
        [Delete("/{employeeId}/current_contract")]
        IObservable<List<Contract>> TerminateCurrentContract(string employeeId);

        /// <summary>
        /// Create a future contract for an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="contract">Future contract</param>
        /// <returns>List of contracts</returns>
        [Post("/{employeeId}/contracts")]
        IObservable<List<Contract>> SaveFutureContract(string employeeId, [Body] Contract contract);

        /// <summary>
        /// Update the future contract of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="contract">Future contract</param>
        /// <returns>List of contracts</returns>
        [Patch("/{employeeId}/contracts")]
        IObservable<List<Contract>> UpdateFutureContract(string employeeId, [Body] Contract contract);

        /// <summary>
        /// Delete the future contract of an employee.
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        [Delete("/{employeeId}/contracts")]
        IObservable<List<Contract>> DeleteFutureContract(string employeeId);
    }
}