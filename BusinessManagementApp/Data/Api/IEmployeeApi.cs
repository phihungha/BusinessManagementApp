using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IEmployeeApi
    {
        IObservable<List<Employee>> GetEmployees();

        IObservable<Employee> GetEmployee(int id);

        IObservable<Employee> AddEmployee(Employee Employee);

        IObservable<Employee> UpdateEmployee(int EmployeeId, Employee request);

        IObservable<object> DeleteEmployee(int id);
    }
}
