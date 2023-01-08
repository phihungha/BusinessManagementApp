using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
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
    public enum BonusRecordsSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Employee name")]
        Name
    }

    public class BonusRecordVM : ViewModelBase
    {
        public ObservableCollection<BonusType> BonusTypes { get; }

        public Employee Employee { get; }

        private BonusType bonusType;

        public BonusType BonusType
        {
            get => bonusType;
            set
            {
                if (value == null)
                    return;
                SetProperty(ref bonusType, value);
                BonusAmount = value.Amount;
            }
        }

        private decimal bonusAmount;

        public decimal BonusAmount
        {
            get => bonusAmount;
            set => SetProperty(ref bonusAmount, value);
        }

        public BonusRecordVM(BonusRecord bonusRecord, BonusesVM parentVM)
        {
            BonusTypes = parentVM.BonusTypes;
            Employee = bonusRecord.Employee;
            bonusType = bonusRecord.Type;
            bonusAmount = bonusRecord.Amount;
        }
    }

    public class BonusesVM : ViewModelBase
    {
        private BonusRecordsRepo bonusesRepo;
        private BonusTypesRepo bonusTypesRepo;
        private EmployeeRepo employeesRepo;

        private List<BonusRecord> bonusRecords = new();
        private List<Employee> employees = new();
        public ObservableCollection<BonusType> BonusTypes { get; } = new();

        private ObservableCollection<BonusRecordVM> bonusRecordVMs { get; } = new();

        public BonusType NoBonusType { get; }

        public ICollectionView BonusesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public BonusRecordsSearchBy SearchBy { get; set; } = BonusRecordsSearchBy.Name;

        #region Time selection

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

        #endregion Time selection

        #region Button enable/disable logic

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand Search { get; }
        public ICommand Save { get; }
        public ICommand Reset { get; }

        #endregion Commands for buttons

        public bool AllowEdit { get; } = false;

        public BonusesVM(BonusRecordsRepo bonusesRepo,
                         BonusTypesRepo bonusTypesRepo,
                         EmployeeRepo employeesRepo,
                         SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageHr)
            {
                AllowEdit = true;
            }

            this.bonusesRepo = bonusesRepo;
            this.bonusTypesRepo = bonusTypesRepo;
            this.employeesRepo = employeesRepo;

            var collectionViewSource = new CollectionViewSource() { Source = bonusRecordVMs };
            BonusesView = collectionViewSource.View;
            BonusesView.Filter = FilterList;

            Search = new RelayCommand(() => BonusesView.Refresh());
            Save = new AsyncRelayCommand(SaveBonusRecords);
            Reset = new RelayCommand(LoadBonusRecordVMs);

            NoBonusType = new BonusType()
            {
                Id = -1,
                Name = "<No bonus>"
            };

            LoadData();
        }

        private bool FilterList(object item)
        {
            var record = (BonusRecordVM)item;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case BonusRecordsSearchBy.Name:
                    return record.Employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case BonusRecordsSearchBy.Id:
                    return record.Employee.Id.ToString() == SearchText;

                default:
                    return false;
            }
        }

        private async Task LoadDependentData()
        {
            BonusTypes.Clear();
            BonusTypes.Add(NoBonusType);
            BonusTypes.AddRange(await bonusTypesRepo.GetBonusTypes());
            employees = await employeesRepo.GetEmployees();
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);

            await LoadDependentData();

            bonusRecords = await bonusesRepo.GetBonusRecords(Year, Month);
            LoadBonusRecordVMs();

            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private void LoadBonusRecordVMs()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            CanSave = false;

            bonusRecordVMs.Clear();

            foreach (Employee employee in employees)
            {
                BonusRecord? record = bonusRecords.Find(i => i.Employee.Id == employee.Id);
                if (record == null)
                {
                    var newRecord = new BonusRecord()
                    {
                        Employee = employee,
                        Type = NoBonusType,
                        Amount = 0
                    };
                    var recordVM = new BonusRecordVM(newRecord, this);
                    bonusRecordVMs.Add(recordVM);
                }
                else
                {
                    var recordVM = new BonusRecordVM(record, this);
                    bonusRecordVMs.Add(recordVM);
                }
            }

            CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task SaveBonusRecords()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);

            List<BonusRecord> recordsToUpdate = bonusRecordVMs.Where(i => i.BonusType != NoBonusType)
                 .Select(i => new BonusRecord()
                 {
                     MonthYear = new DateTime(Year, Month, 1),
                     Employee = i.Employee,
                     Type = i.BonusType,
                     Amount = i.BonusType.Amount
                 })
                 .ToList();

            bonusRecords = await bonusesRepo.UpdateBonusRecords(Year, Month, recordsToUpdate);
            await LoadDependentData();
            LoadBonusRecordVMs();

            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}