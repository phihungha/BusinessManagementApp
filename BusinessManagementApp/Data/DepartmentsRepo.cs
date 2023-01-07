using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class DepartmentsRepo
    {
        private IDepartmentsApi api;

        public DepartmentsRepo(IDepartmentsApi api)
        {
            this.api = api;
        }

        public IObservable<Department> GetDepartment(int id)
        {
            return api.GetDepartment(id);
        }

        public IObservable<List<Department>> GetDepartments()
        {
            return api.GetDepartments();
        }

        public IObservable<Department> AddDepartment(Department department)
        {
            return api.SaveDepartment(department);
        }

        public IObservable<Department> UpdateDepartment(int departmentId, Department department)
        {
            return api.UpdateDepartment(departmentId, department);
        }

        public IObservable<object> DeleteDepartment(int id)
        {
            return api.DeleteDepartment(id);
        }
    }
}