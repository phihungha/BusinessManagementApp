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
    public enum SkillTypeInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }

    public class SkillTypesVM : ViewModelBase
    {
        private SkillTypesRepo skillTypeRepo;

        private ObservableCollection<SkillType> skillTypes { get; } = new();

        public ICollectionView SkillTypesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public SkillTypeInfoSearchBy SearchBy { get; set; } = SkillTypeInfoSearchBy.Name;

        public ICommand AddSkillType { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public SkillTypesVM(SkillTypesRepo skillTypesRepo)
        {
            this.skillTypeRepo = skillTypesRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = skillTypes };
            SkillTypesView = collectionViewSource.View;
            SkillTypesView.Filter = FilterList;

            AddSkillType = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillTypeDetails));
            Search = new RelayCommand(() => SkillTypesView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var skillType = (SkillType)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case SkillTypeInfoSearchBy.Name:
                    return skillType.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SkillTypeInfoSearchBy.Id:
                    return skillType.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);


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

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.SkillTypeDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            skillTypes.AddRange(await skillTypeRepo.GetSkillTypes());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}