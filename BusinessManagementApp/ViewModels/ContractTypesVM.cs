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
    public enum ContractTypeInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }
    public class ContractTypesVM : ViewModelBase
    {
        private ContractTypesRepo contracttypesRepo;

        private ObservableCollection<ContractType> contracttypes { get; } = new();

        public ICollectionView ContractTypesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ContractTypeInfoSearchBy SearchBy { get; set; } = ContractTypeInfoSearchBy.Name;

        public ICommand AddContractType { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }
        public ContractTypesVM(ContractTypesRepo contracttypesRepo)
        {
            this.contracttypesRepo = contracttypesRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = contracttypes };
            ContractTypesView = collectionViewSource.View;
            ContractTypesView.Filter = FilterList;
            
            AddContractType = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypeDetails));
            Search = new RelayCommand(() => ContractTypesView.Refresh());
            Edit = new RelayCommand<int>(id => OpenDetailsView(id.ToString()));
            LoadData();
        }
        private bool FilterList(object item)
        {
            var contracttype = (ContractType)item;

            switch (SearchBy)
            {
                case ContractTypeInfoSearchBy.Name:
                    return contracttype.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ContractTypeInfoSearchBy.Id:
                    return contracttype.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.ContractTypeDetails, id);
        }

        private async void LoadData()
        {
            contracttypes.AddRange(await contracttypesRepo.GetContractTypes());
        }
    }

}
