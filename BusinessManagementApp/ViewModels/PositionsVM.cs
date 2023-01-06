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
    public enum PositionInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }

    public class PositionsVM : ViewModelBase
    {
        private PositionsRepo positionsRepo;

        private ObservableCollection<Position> positions { get; } = new();

        public ICollectionView PositionsView { get; }

        public string SearchText { get; set; } = string.Empty;

        public PositionInfoSearchBy SearchBy { get; set; } = PositionInfoSearchBy.Name;

        public ICommand AddPosition { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public PositionsVM(PositionsRepo positionsRepo, SessionsRepo sessionsRepo)
        {
            this.positionsRepo = positionsRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = positions };
            PositionsView = collectionViewSource.View;
            PositionsView.Filter = FilterList;

            AddPosition = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.PositionDetails));
            Search = new RelayCommand(() => PositionsView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var position = (Position)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case PositionInfoSearchBy.Name:
                    return position.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case PositionInfoSearchBy.Id:
                    return position.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.PositionDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            positions.AddRange(await positionsRepo.GetPositions());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}