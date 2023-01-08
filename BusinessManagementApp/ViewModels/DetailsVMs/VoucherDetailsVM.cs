using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class VoucherDetailsVM : ViewModelBase
    {
        #region Dependencies

        private VouchersRepo vouchersRepo;
        private VoucherTypesRepo voucherTypesRepo;

        #endregion Dependencies

        #region Combobox items

        public ObservableCollection<VoucherType> VoucherTypes { get; } = new();

        #endregion Combobox items

        #region Input properties

        private int number = 1;

        public int Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        private string code = string.Empty;

        public string Code
        {
            get => code;
            private set => SetProperty(ref code, value);
        }

        private VoucherType voucherType = new();

        // Binding combobox directly to non-enum properties will only work
        // when the class implements IEquatable.
        // Check Department class for an example.
        public VoucherType VoucherType
        {
            get => voucherType;
            set => SetProperty(ref voucherType, value);
        }

        private DateTime releaseDate = DateTime.Now;

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set => SetProperty(ref releaseDate, value, true);
        }

        private DateTime expiryDate = DateTime.Now.AddMonths(3);

        public DateTime ExpiryDate
        {
            get => expiryDate;
            set => SetProperty(ref expiryDate, value, true);
        }

        #endregion Input properties

        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public bool AllowAdd { get; } = false;

        public VoucherDetailsVM(VouchersRepo vouchersRepo,
                                VoucherTypesRepo voucherTypesRepo,
                                SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageSales)
            {
                AllowAdd = true;
            }

            this.vouchersRepo = vouchersRepo;
            this.voucherTypesRepo = voucherTypesRepo;

            Save = new AsyncRelayCommand(GenerateVouchers);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Vouchers)
                );

            LoadData();
        }

        public async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            VoucherTypes.AddRange(await voucherTypesRepo.GetVoucherTypes());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task GenerateVouchers()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            BusyIndicatorUtils.SetBusyIndicator(true);

            var voucher = new Voucher()
            {
                Code = Code,
                Type = VoucherType,
                ReleaseDate = ReleaseDate,
                ExpiryDate = ExpiryDate,
            };

            await vouchersRepo.CreateVouchers(voucher, Number);
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Vouchers);
        }
    }
}