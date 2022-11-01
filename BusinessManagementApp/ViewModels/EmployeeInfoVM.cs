using BusinessManagementApp.Data;
using BusinessManagementApp.Repositories;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class EmployeeInfoVM : ObservableObject
    {
        public EmployeesRepository employeesRepository;

        public ObservableCollection<Employee> Employees { get; set; } = new();

        public ICommand AddEmployee { get; private set; }

        public EmployeeInfoVM(EmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;

            AddEmployee = new RelayCommand(
                () => WorkspaceNavUtils.NavigateToWithId(WorkspaceViewName.EmployeeInfoDetails, 1)
                );

            LoadData();
        }

        private void LoadData()
        {
            Employees.AddRange(employeesRepository.GetAllEmployees());
        }
    }
}
