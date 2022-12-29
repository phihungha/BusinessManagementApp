using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class VoucherTypeDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private VoucherTypesRepo voucherTypesRepo;

        #endregion Dependencies

        public ObservableCollection<Product> SelectedProducts { get; set; } = new();


        // Properties for inputs on the screen
        // Remember to declare validation attributes when appropriate.
        // List of validation attributes: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0
        // Check ViewModels/ValidationAttributes.cs for custom validation attributes.
        #region Input properties

        private int id = 0;

        public int Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }

        private string name = string.Empty;

        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string description = string.Empty;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value, true);
        }
        private decimal minNetPrice = 10000;
        public decimal MinNetPrice
        {
            get => minNetPrice;
            set => SetProperty(ref minNetPrice, value, true);
        }

        private DiscountType discountType = DiscountType.Percent;

        public DiscountType DiscountType
        {
            get => discountType;
            set => SetProperty(ref discountType, value, true);
        }

        private double discountValue = 0;

        public double DiscountValue
        {
            get => discountValue;
            set => SetProperty(ref discountValue, value, true);
        }

        private List<Product> appliedProducts = new List<Product>();
        public List<Product> AppliedProducts
        {
            get => appliedProducts;
            set => SetProperty(ref appliedProducts, value, true);
        }

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEditMode = false;

        private bool IsEditMode
        {
            get => isEditMode;
            set
            {
                SetProperty(ref isEditMode, value);
                CanDelete = value;
            }
        }

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        private bool canDelete = false;

        public bool CanDelete
        {
            get => canDelete;
            private set => SetProperty(ref canDelete, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons
        public ICommand SelectProducts { get; }
        public ICommand Save { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public VoucherTypeDetailsVM(VoucherTypesRepo voucherTypesRepo)
        {
            this.voucherTypesRepo = voucherTypesRepo;
            SelectProducts = new RelayCommand(ExecuteSelectProducts);
            Save = new AsyncRelayCommand(SaveVoucherType);
            Delete = new AsyncRelayCommand(DeleteVoucherType);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.VoucherTypes)
                );

        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        private void ExecuteSelectProducts()
        {
            var introMessage = "Select products item for voucher type";
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(WorkspaceViewName.SelectProducts, introMessage);
        }
        public override void OnBack(object? extra = null)
        {
            if (extra == null)
            {
                throw new ArgumentException(nameof(extra));
            }
            SelectedProducts.ClearAndAddRange((List<Product>)extra);
            AppliedProducts = SelectedProducts.ToList();
        }
        public override async void LoadData(object? id = null)
        {
            //Products.AddRange(await productsRepo.GetProducts());

            if (id != null)
            {
                IsEditMode = true;
                await LoadVoucherType((int)id);
            }

            CanSave = true;
        }

        private async Task LoadVoucherType(int id)
        {
            VoucherType voucherType = await voucherTypesRepo.GetVoucherType(id);
            Id = voucherType.Id;
            Name = voucherType.Name;
            DiscountType = voucherType.DiscountType;
            DiscountValue = voucherType.DiscountValue;
            AppliedProducts = voucherType.AppliedProducts;
            MinNetPrice = voucherType.MinNetPrice;
            SelectedProducts = new ObservableCollection<Product>(AppliedProducts);
        }

        private async Task SaveVoucherType()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var voucherType = new VoucherType()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                DiscountValue = DiscountValue,
                AppliedProducts = AppliedProducts,
                DiscountType = DiscountType,
                MinNetPrice = MinNetPrice
            };

            if (IsEditMode)
            {
                await voucherTypesRepo.UpdateVoucherType(Id, voucherType);
            }
            else
            {
                await voucherTypesRepo.AddVoucherType(voucherType);
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.VoucherTypes);
        }

        private async Task DeleteVoucherType()
        {
            await voucherTypesRepo.DeleteVoucherType(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.VoucherTypes);
        }
    }
}