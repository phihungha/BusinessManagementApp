﻿using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
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
        private ProvidersRepo providerRepo;

        private ObservableCollection<Provider> providers { get; } = new();

        public ICollectionView ProvidersView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ProviderInfoSearchBy SearchBy { get; set; } = ProviderInfoSearchBy.Name;

        public ICommand AddProvider { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public ProvidersVM(ProvidersRepo providerRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageSales)
            {
                AllowAdd = true;
            }

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
            if (SearchText == null)
                return true;

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
            BusyIndicatorUtils.SetBusyIndicator(true);
            providers.AddRange(await providerRepo.GetProviders());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}