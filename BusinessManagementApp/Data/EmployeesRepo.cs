using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class EmployeesRepo
    {
        private IEmployeeApi api;

        public EmployeesRepo(IEmployeeApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeleteEmployee(int id)
        {
            return api.DeleteEmployee(id);
        }

        public IObservable<Employee> GetEmployee(int id)
        {
            return api.GetEmployee(id);
        }

        public IObservable<List<Employee>> GetEmployees()
        {
            return api.GetEmployees();
        }

        public IObservable<Employee> AddEmployee(Employee Employee)
        {
            return api.AddEmployee(Employee);
        }

        public IObservable<Employee> UpdateEmployee(int employeeId, Employee request)
        {
            return api.UpdateEmployee(employeeId, request);
        }
    }
}