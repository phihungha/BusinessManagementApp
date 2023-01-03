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
        private ContractTypesRepo contractTypesRepo;

        private ObservableCollection<ContractType> contracttypes { get; } = new();

        public ICollectionView ContractTypesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ContractTypeInfoSearchBy SearchBy { get; set; } = ContractTypeInfoSearchBy.Name;

        public ICommand AddContractType { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public ContractTypesVM(ContractTypesRepo contractTypesRepo)
        {
            this.contractTypesRepo = contractTypesRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = contracttypes };
            ContractTypesView = collectionViewSource.View;
            ContractTypesView.Filter = FilterList;

            AddContractType = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypeDetails));
            Search = new RelayCommand(() => ContractTypesView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var contractType = (ContractType)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case ContractTypeInfoSearchBy.Name:
                    return contractType.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ContractTypeInfoSearchBy.Id:
                    return contractType.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.ContractTypeDetails, id);
        }

        private async void LoadData()
        {
            contracttypes.AddRange(await contractTypesRepo.GetContractTypes());
        }
    }
}