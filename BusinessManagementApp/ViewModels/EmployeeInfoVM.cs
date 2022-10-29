using BusinessManagementApp.Data;
using BusinessManagementApp.Repositories;
using BusinessManagementApp.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels
{
    public class EmployeeInfoVM : ObservableObject
    {
        public EmployeesRepository employeesRepository = new();

        public ObservableCollection<Employee> Employees { get; set; } = new();

        public EmployeeInfoVM()
        {
            LoadData();
        }

        private void LoadData()
        {
            Employees.AddRange(employeesRepository.GetAllEmployees());
        }
    }
}
