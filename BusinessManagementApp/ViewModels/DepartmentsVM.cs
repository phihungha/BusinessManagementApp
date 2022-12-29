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
    public enum DepartmentInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Phone number")]
        PhoneNumber,

        [Description("Header")]
        Header
    }

    public class DepartmentsVM : ViewModelBase
    {
        private DepartmentsRepo departmentsRepo;

        private ObservableCollection<Department> departments { get; } = new();

        public ICollectionView DepartmentsView { get; }

        public string SearchText { get; set; } = string.Empty;

        public DepartmentInfoSearchBy SearchBy { get; set; } = DepartmentInfoSearchBy.Name;

        public ICommand AddDepartment { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public DepartmentsVM(DepartmentsRepo departmentsRepo)
        {
            this.departmentsRepo = departmentsRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = departments };
            DepartmentsView = collectionViewSource.View;
            DepartmentsView.Filter = FilterList;

            AddDepartment = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.DepartmentDetails));
            Search = new RelayCommand(() => DepartmentsView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var department = (Department)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case DepartmentInfoSearchBy.Name:
                    return department.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case DepartmentInfoSearchBy.Id:
                    return department.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);


                case DepartmentInfoSearchBy.PhoneNumber:
                    return department.PhoneNumber.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private void OpenDetailsView(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("ID is null");
            }

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.DepartmentDetails, id);
        }

        private async void LoadData()
        {
            departments.AddRange(await departmentsRepo.GetDepartments());
        }
    }
}