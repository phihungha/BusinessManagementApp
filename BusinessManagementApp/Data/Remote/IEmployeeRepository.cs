using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IEmployeeRepository
    {

        //Employee
        IObservable<List<Employee>> GetEmployees();

        IObservable<Employee> GetEmployee(int id);

        IObservable<Employee> SaveEmployee(Employee Employee);

        IObservable<Employee> UpdateEmployee(int EmployeeId, Employee request);

        IObservable<Object> DeleteEmployee(int id);

        //Skill

    }
}
