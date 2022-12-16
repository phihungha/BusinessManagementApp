using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Data;
using System.Reactive.Linq;
using BusinessManagementApp.Utils;

namespace BusinessManagementApp.ViewModels
{
    public enum CustomerInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Address")]
        Address,

        [Description("Phone")]
        Phone
    }
    public class CustomersVM : ViewModelBase
    {
        private CustomersRepo customersRepo;

        private ObservableCollection<Customer> customers { get; } = new();

        public ICollectionView CustomersView { get; }

        public string SearchText { get; set; } = string.Empty;

        public CustomerInfoSearchBy SearchBy { get; set; } = CustomerInfoSearchBy.Name;

        public ICommand AddCustomer { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public CustomersVM(CustomersRepo customersRepo)
        {
            this.customersRepo = customersRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = customers };
            CustomersView = collectionViewSource.View;
            CustomersView.Filter = FilterList;

            AddCustomer = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.CustomerDetails));
            Search = new RelayCommand(() => CustomersView.Refresh());
            Edit = new RelayCommand<string>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var customer = (Customer)item;

            switch (SearchBy)
            {
                case CustomerInfoSearchBy.Name:
                    return customer.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case CustomerInfoSearchBy.Id:
                    return customer.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case CustomerInfoSearchBy.Address:
                    return customer.Address.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case CustomerInfoSearchBy.Phone:
                    return customer.Phone.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            // Navigate to details screen
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.CustomerDetails, id);
        }

        private async void LoadData()
        {
            customers.AddRange(await customersRepo.GetCustomers());
        }
    }
}
