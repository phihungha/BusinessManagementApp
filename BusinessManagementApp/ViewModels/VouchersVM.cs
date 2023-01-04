using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum VoucherInfoSearchBy
    {
        [Description("Code")]
        Code,

        [Description("Type")]
        Type
    }

    public class VouchersVM : ViewModelBase
    {
        private VouchersRepo vouchersRepo;

        private ObservableCollection<Voucher> vouchers { get; } = new();

        public ICollectionView VouchersView { get; }

        public string SearchText { get; set; } = string.Empty;

        public VoucherInfoSearchBy SearchBy { get; set; } = VoucherInfoSearchBy.Code;

        private List<Voucher> selectedVouchers = new();

        public List<Voucher> SelectedVouchers
        {
            get => selectedVouchers;
            set => SetProperty(ref selectedVouchers, value);
        }

        public ICommand AddVouchers { get; }
        public ICommand Search { get; }
        public ICommand DeleteVouchers { get; }

        public VouchersVM(VouchersRepo vouchersRepo)
        {
            this.vouchersRepo = vouchersRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = vouchers };
            VouchersView = collectionViewSource.View;
            VouchersView.Filter = FilterList;
            var Ids = SelectedVouchers.Select(x => x.Code).ToList();
            AddVouchers = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.VoucherDetails));
            Search = new RelayCommand(() => VouchersView.Refresh());
            DeleteVouchers = new AsyncRelayCommand(ExecuteDelete);

            LoadData();
        }

        private bool FilterList(object item)
        {
            var voucher = (Voucher)item;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case VoucherInfoSearchBy.Code:
                    return voucher.Code.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case VoucherInfoSearchBy.Type:
                    return voucher.Type.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private async Task ExecuteDelete()
        {
            var Ids = SelectedVouchers.Select(x => x.Code).ToList();
            await vouchersRepo.DeleteVouchers(SelectedVouchers.Select(x => x.Code).ToList());
        }

        private async void LoadData()
        {
            vouchers.AddRange(await vouchersRepo.GetVouchers());
        }
    }
}