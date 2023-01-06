using BusinessManagementApp.Data;
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
    public enum VoucherTypeInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }

    public class VoucherTypesVM : ViewModelBase
    {
        private VoucherTypesRepo voucherTypesRepo;

        private ObservableCollection<VoucherType> voucherTypes { get; } = new();

        public ICollectionView VoucherTypesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public VoucherTypeInfoSearchBy SearchBy { get; set; } = VoucherTypeInfoSearchBy.Name;

        public ICommand AddVoucherType { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public VoucherTypesVM(VoucherTypesRepo voucherTypesRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageConfig)
            {
                AllowAdd = true;
            }

            this.voucherTypesRepo = voucherTypesRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = voucherTypes };
            VoucherTypesView = collectionViewSource.View;
            VoucherTypesView.Filter = FilterList;

            AddVoucherType = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.VoucherTypeDetails));
            Search = new RelayCommand(() => VoucherTypesView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var voucherType = (VoucherType)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case VoucherTypeInfoSearchBy.Name:
                    return voucherType.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case VoucherTypeInfoSearchBy.Id:
                    return voucherType.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            // Navigate to details screen
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.VoucherTypeDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            voucherTypes.AddRange(await voucherTypesRepo.GetVoucherTypes());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}