using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum BonusTypesSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Description")]
        Description
    }

    public class BonusTypesVM : ViewModelBase
    {
        private BonusTypesRepo bonusTypesRepo;

        private ObservableCollection<BonusType> bonusTypes { get; } = new();

        public ICollectionView BonusTypesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public BonusTypesSearchBy SearchBy { get; set; } = BonusTypesSearchBy.Name;

        public ICommand Add { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public BonusTypesVM(BonusTypesRepo bonusTypesRepo)
        {
            this.bonusTypesRepo = bonusTypesRepo;

            var collectionViewSource = new CollectionViewSource() { Source = bonusTypes };
            BonusTypesView = collectionViewSource.View;
            BonusTypesView.Filter = FilterList;

            Add = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.BonusTypeDetails));
            Search = new RelayCommand(() => BonusTypesView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var bonusType = (BonusType)item;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case BonusTypesSearchBy.Name:
                    return bonusType.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case BonusTypesSearchBy.Id:
                    return bonusType.Id.ToString() == SearchText;

                case BonusTypesSearchBy.Description:
                    return bonusType.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.BonusTypeDetails, id);
        }

        private async void LoadData()
        {
            bonusTypes.AddRange(await bonusTypesRepo.GetBonusTypes());
        }
    }
}