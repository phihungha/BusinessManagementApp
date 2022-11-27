using BusinessManagementApp.Data.Model;
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

        public ObservableCollection<Employee> Employees { get; } = new();

        public ICommand AddEmployee { get; }

        public EmployeeInfoVM(EmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;

            AddEmployee = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfoDetails)
                );

            LoadData();
        }

        private async void LoadData()
        {
            Employees.AddRange(await employeesRepository.GetAllEmployees());
        }
    }
}
