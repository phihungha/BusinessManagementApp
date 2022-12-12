using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum EmployeeInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Citizen ID")]
        CitizenId,

        [Description("Phone number")]
        PhoneNumber
    }

    public class EmployeeInfoVM : ViewModelBase
    {
        private EmployeeRepo employeesRepo;

        private ObservableCollection<Employee> employees { get; } = new();

        public ICollectionView EmployeesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public EmployeeInfoSearchBy SearchBy { get; set; } = EmployeeInfoSearchBy.Name;

        public ICommand AddEmployee { get; }
        public ICommand Search { get; }

        public EmployeeInfoVM(EmployeeRepo employeesRepo)
        {
            this.employeesRepo = employeesRepo;

            var collectionViewSource = new CollectionViewSource() { Source = employees };
            EmployeesView = collectionViewSource.View;
            EmployeesView.Filter = FilterList;

            AddEmployee = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfoDetails));
            Search = new RelayCommand(() => EmployeesView.Refresh());

            LoadData();
        }

        private bool FilterList(object item)
        {
            var employee = (Employee)item;

            switch (SearchBy)
            {
                case EmployeeInfoSearchBy.Name:
                    return employee.Name.Contains(SearchText);

                case EmployeeInfoSearchBy.Id:
                    return employee.Id.Contains(SearchText);

                case EmployeeInfoSearchBy.CitizenId:
                    return employee.CitizenId.Contains(SearchText);

                case EmployeeInfoSearchBy.PhoneNumber:
                    return employee.PhoneNumber.Contains(SearchText);

                default:
                    return false;
            }
        }

        private async void LoadData()
        {
            employees.AddRange(await employeesRepo.GetEmployees());
        }
    }
}