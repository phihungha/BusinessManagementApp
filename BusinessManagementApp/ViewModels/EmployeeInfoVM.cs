using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data.Remote;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class EmployeeInfoVM : ObservableObject
    {
        public IEmployeeApi employeesRepository;

        public ObservableCollection<Employee> Employees { get; } = new();

        public ICommand AddEmployee { get; }

        public EmployeeInfoVM(IEmployeeApi employeesRepository)
        {
            this.employeesRepository = employeesRepository;

            AddEmployee = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfoDetails)
                );

            LoadData();
        }

        private async void LoadData()
        {
            Employees.AddRange(await employeesRepository.GetEmployees());
        }
    }
}