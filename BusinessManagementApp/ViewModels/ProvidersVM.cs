using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum ProviderInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }

    public class ProvidersVM : ViewModelBase
    {
        private ProviderRepo providerRepo;

        private ObservableCollection<Provider> providers { get; } = new();

        public ICollectionView ProvidersView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ProviderInfoSearchBy SearchBy { get; set; } = ProviderInfoSearchBy.Name;

        public ICommand AddProvider { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public ProvidersVM(ProviderRepo providerRepo)
        {
            this.providerRepo = providerRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = providers };
            ProvidersView = collectionViewSource.View;
            ProvidersView.Filter = FilterList;

            AddProvider = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProviderDetails));
            Search = new RelayCommand(() => ProvidersView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var provider = (Provider)item;

            switch (SearchBy)
            {
                case ProviderInfoSearchBy.Name:
                    return provider.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProviderInfoSearchBy.Id:
                    return provider.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);


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

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.ProviderDetails, id);
        }

        private async void LoadData()
        {
            providers.AddRange(await providerRepo.GetProviders());
        }
    }
}