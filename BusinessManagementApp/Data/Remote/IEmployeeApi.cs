using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IEmployeeApi
    {

        //Employee
        IObservable<List<Employee>> GetEmployees();

        IObservable<Employee> GetEmployee(int id);

        IObservable<Employee> SaveEmployee(Employee Employee);

        IObservable<Employee> UpdateEmployee(int EmployeeId, Employee request);

        IObservable<object> DeleteEmployee(int id);

        //Skill

    }
}
