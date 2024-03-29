﻿using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.DetailsVMs;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum OvertimeOverviewSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name
    }

    public class OvertimeVM : ViewModelBase
    {
        private OvertimeRecordsRepo overtimeRepo;

        private ObservableCollection<OvertimeOverview> overtimeOverviews { get; } = new();

        public ICollectionView OvertimeOverviewView { get; }

        public string SearchText { get; set; } = string.Empty;

        public int[] MonthSelections { get; } = Enumerable.Range(1, 12).ToArray();

        private int month = DateTime.Now.Month;

        public int Month
        {
            get => month;
            set
            {
                SetProperty(ref month, value);
                LoadData();
            }
        }

        private int year = DateTime.Now.Year;

        public int Year
        {
            get => year;
            set
            {
                SetProperty(ref year, value);
                LoadData();
            }
        }

        public int MaxYear { get; } = DateTime.Now.Year;

        public OvertimeOverviewSearchBy SearchBy { get; set; } = OvertimeOverviewSearchBy.Name;

        public ICommand Search { get; }
        public ICommand Edit { get; }

        public OvertimeVM(OvertimeRecordsRepo overtimeRecordsRepo)
        {
            this.overtimeRepo = overtimeRecordsRepo;

            var collectionViewSource = new CollectionViewSource() { Source = overtimeOverviews };
            OvertimeOverviewView = collectionViewSource.View;
            OvertimeOverviewView.Filter = FilterList;

            Search = new RelayCommand(() => OvertimeOverviewView.Refresh());
            Edit = new RelayCommand<string>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var employee = ((OvertimeOverview)item).Employee;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case OvertimeOverviewSearchBy.Name:
                    return employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case OvertimeOverviewSearchBy.Id:
                    return employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private void OpenDetailsView(string? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parameter = new OvertimeDetailsParameter()
            {
                MonthYear = new DateTime(Year, Month, 1),
                EmployeeId = id
            };

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.OvertimeDetails, parameter);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            overtimeOverviews.ClearAndAddRange(await overtimeRepo.GetOvertimeOverviews(Year, Month));
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}