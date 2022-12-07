using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class EmployeeInfoVM : ViewModelBase
    {
        private EmployeeRepo employeesRepo;

        public ObservableCollection<Employee> Employees { get; } = new();

        public ICommand AddEmployee { get; }

        public EmployeeInfoVM(EmployeeRepo employeesRepo)
        {
            this.employeesRepo = employeesRepo;

            AddEmployee = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfoDetails));

            LoadData();
        }

        private async void LoadData()
        {
            Employees.AddRange(await employeesRepo.GetEmployees());
        }
    }
}