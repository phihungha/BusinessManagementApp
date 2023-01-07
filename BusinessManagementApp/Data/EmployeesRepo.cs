using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class EmployeeRepo
    {
        private IEmployeesApi api;

        public EmployeeRepo(IEmployeesApi api)
        {
            this.api = api;
        }

        public IObservable<Employee> GetEmployee(string id)
        {
            return api.GetEmployee(id);
        }

        public IObservable<List<Employee>> GetEmployees()
        {
            return api.GetEmployees();
        }

        public IObservable<Employee> AddEmployee(Employee employee)
        {
            return api.SaveEmployee(employee);
        }

        public IObservable<Employee> UpdateEmployee(string employeeId, Employee employee)
        {
            return api.UpdateEmployee(employeeId, employee);
        }

        public IObservable<object> DeleteEmployee(string id)
        {
            return api.DeleteEmployee(id);
        }

        public IObservable<List<Contract>> AddFutureContract(string employeeId, Contract contract)
        {
            return api.SaveFutureContract(employeeId, contract);
        }

        public IObservable<List<Contract>> TerminateCurrentContract(string employeeId)
        {
            return api.TerminateCurrentContract(employeeId);
        }

        public IObservable<List<Contract>> DeleteFutureContract(string employeeId)
        {
            return api.DeleteFutureContract(employeeId);
        }
    }
}