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
    public enum SkillOverviewSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name
    }

    public class SkillRatingVM : ViewModelBase
    {
        private SkillRecordsRepo skillsRepo;

        private ObservableCollection<SkillOverview> skillOverviews { get; } = new();

        public ICollectionView SkillOverviewView { get; }

        public string SearchText { get; set; } = string.Empty;

        public SkillOverviewSearchBy SearchBy { get; set; } = SkillOverviewSearchBy.Name;

        public ICommand Search { get; }
        public ICommand Rate { get; }

        public SkillRatingVM(SkillRecordsRepo skillsRepo)
        {
            this.skillsRepo = skillsRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = skillOverviews };
            SkillOverviewView = collectionViewSource.View;
            SkillOverviewView.Filter = FilterList;

            Search = new RelayCommand(() => SkillOverviewView.Refresh());
            Rate = new RelayCommand<string>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var employee = ((SkillOverview)item).Employee;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SkillOverviewSearchBy.Name:
                    return employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SkillOverviewSearchBy.Id:
                    return employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            // Navigate to details screen
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.SkillRatingDetails, id);
        }

        private async void LoadData()
        {
            skillOverviews.AddRange(await skillsRepo.GetSkillOverviews());
        }
    }
}