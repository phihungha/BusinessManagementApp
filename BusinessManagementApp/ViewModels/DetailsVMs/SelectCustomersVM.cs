using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public enum SelectCustomersSearchBy
    {
        [Description("Name")]
        Name,

        [Description("Address")]
        Address,

        [Description("Phone")]
        Phone
    }

    internal class SelectCustomersVM : ViewModelBase
    {
        private CustomersRepo customersRepo;
        private ObservableCollection<Customer> customers { get; } = new();
        public ICollectionView CustomersView { get; }
        public string SearchText { get; set; } = string.Empty;
        public SelectCustomersSearchBy SearchBy { get; set; } = SelectCustomersSearchBy.Name;

        private Customer selectedCustomer = new();

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set => SetProperty(ref selectedCustomer, value);
        }

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        #region Commands for buttons

        public ICommand Search { get; }
        public ICommand Select { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public SelectCustomersVM(CustomersRepo customersRepo)
        {
            this.customersRepo = customersRepo;

            var collectionViewSource = new CollectionViewSource() { Source = customers };
            CustomersView = collectionViewSource.View;
            CustomersView.Filter = FilterList;

            Select = new RelayCommand(() => WorkspaceNavUtils.NavigateBackWithExtra(SelectedCustomer));
            Search = new RelayCommand(() => CustomersView.Refresh());
            Cancel = new RelayCommand(() => WorkspaceNavUtils.NavigateBack());
        }

        private bool FilterList(object item)
        {
            var customer = (Customer)item;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SelectCustomersSearchBy.Name:
                    return customer.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SelectCustomersSearchBy.Address:
                    return customer.Address.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SelectCustomersSearchBy.Phone:
                    return customer.Phone.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        public override async void LoadData(object? input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Title = (string)input;
            customers.AddRange(await customersRepo.GetCustomers());
        }
    }
}