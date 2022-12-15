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
using BusinessManagementApp.Data.Api;

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

    public class EmployeesVM : ViewModelBase
    {
        private EmployeeRepo employeesRepo;
        private readonly IEmployeeApi employeeApi;

        private ObservableCollection<Employee> employees { get; } = new();

        public ICollectionView EmployeesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public EmployeeInfoSearchBy SearchBy { get; set; } = EmployeeInfoSearchBy.Name;

        public ICommand AddEmployee { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public EmployeesVM(EmployeeRepo employeesRepo, IEmployeeApi employeeApi)
        {
            this.employeesRepo = employeesRepo;
            this.employeeApi = employeeApi;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = employees };
            EmployeesView = collectionViewSource.View;
            EmployeesView.Filter = FilterList;

            AddEmployee = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfoDetails));
            Search = new RelayCommand(() => EmployeesView.Refresh());
            Edit = new RelayCommand<string>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var employee = (Employee)item;

            switch (SearchBy)
            {
                case EmployeeInfoSearchBy.Name:
                    return employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case EmployeeInfoSearchBy.Id:
                    return employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case EmployeeInfoSearchBy.CitizenId:
                    return employee.CitizenId.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case EmployeeInfoSearchBy.PhoneNumber:
                    return employee.PhoneNumber.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private void OpenDetailsView(string? id)
        {
            if (id == null)
            {
                throw new ArgumentException("ID is null");
            }

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.EmployeeInfoDetails, id);
        }

        private async void LoadData()
        {
            // employees.AddRange(await employeesRepo.GetEmployees());
            employees.AddRange(await employeeApi.GetEmployees());
        }

    }
}