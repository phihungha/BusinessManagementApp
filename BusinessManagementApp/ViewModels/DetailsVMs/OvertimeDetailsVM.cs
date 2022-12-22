using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public struct OvertimeDetailsParameter
    {
        public string EmployeeId { get; set; }
        public DateTime MonthYear { get; set; }
    }

    public class OvertimeRecordVM : ViewModelBase, IEditableObject
    {
        private OvertimeDetailsVM parentVM;

        private decimal OvertimeHourlyRate => parentVM.OvertimeHourlyRate;
        public int MaxNumOfOvertimeHours => parentVM.MaxNumOfOvertimeHours;

        public int Day { get; }

        private int oldNumOfHours = 0;

        private int numOfHours = 0;

        public int NumOfHours
        {
            get => numOfHours;
            set
            {
                SetProperty(ref numOfHours, value);
                CalculateOvertimePay(value);
            }
        }

        private decimal overtimePay = 0;

        public decimal OvertimePay
        {
            get => overtimePay;
            set
            {
                SetProperty(ref overtimePay, value);
                parentVM.CalculateTotalOvertimePay();
            }
        }

        private bool isEditing = false;

        public OvertimeRecordVM(int day, OvertimeDetailsVM parentVM)
        {
            this.parentVM = parentVM;
            Day = day;
        }

        private void CalculateOvertimePay(int numOfHours)
        {
            OvertimePay = OvertimeHourlyRate * numOfHours;
        }

        public void BeginEdit()
        {
            if (!isEditing)
            {
                oldNumOfHours = NumOfHours;
                isEditing = true;
            }
        }

        public void CancelEdit()
        {
            if (isEditing)
            {
                NumOfHours = oldNumOfHours;
                isEditing = false;
            }
        }

        public void EndEdit()
        {
            if (isEditing)
            {
                oldNumOfHours = NumOfHours;
                isEditing = false;
            }
        }
    }

    public class OvertimeDetailsVM : ViewModelBase
    {
        #region Dependencies

        private OvertimeRepo overtimeRepo;

        #endregion Dependencies

        public ObservableCollection<OvertimeRecordVM> OvertimeRecordVMs { get; } = new();

        public int SelectedRecordIndex { get; } = DateTime.Now.Day - 1;

        private Employee currentEmployee = new();

        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set => SetProperty(ref currentEmployee, value);
        }

        private DateTime currentMonthYear = new DateTime();

        public DateTime CurrentMonthYear
        {
            get => currentMonthYear;
            set => SetProperty(ref currentMonthYear, value);
        }

        private decimal totalOvertimePay = 0;

        public decimal TotalOvertimePay
        {
            get => totalOvertimePay;
            set => SetProperty(ref totalOvertimePay, value);
        }

        public int MaxNumOfOvertimeHours = 0;
        public decimal OvertimeHourlyRate = 0;

        #region Button enable/disable logic

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public OvertimeDetailsVM(OvertimeRepo overtimeRepo, ConfigRepo configRepo)
        {
            this.overtimeRepo = overtimeRepo;

            MaxNumOfOvertimeHours = configRepo.Config.MaxNumOfOvertimeHours;
            OvertimeHourlyRate = configRepo.Config.OvertimeHourlyRate;

            Save = new AsyncRelayCommand(SaveOvertimeRecords);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Overtime)
                );
        }

        public override async void LoadData(object? id = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var parameter = (OvertimeDetailsParameter)id;
            OvertimeOverview overview = await overtimeRepo.GetOvertimeDetails(parameter.EmployeeId,
                                                                              parameter.MonthYear.Year,
                                                                              parameter.MonthYear.Month);
            CurrentEmployee = overview.Employee;
            CurrentMonthYear = parameter.MonthYear;
            LoadOvertimeRecordVMs(overview.Records);

            CanSave = true;
        }

        public void CalculateTotalOvertimePay()
        {
            TotalOvertimePay = OvertimeRecordVMs.Sum(i => i.OvertimePay);
        }

        private void LoadOvertimeRecordVMs(List<OvertimeRecord> records)
        {
            int numberOfDaysInMonth = DateTime.DaysInMonth(CurrentMonthYear.Year, CurrentMonthYear.Month);
            for (int day = 1; day <= numberOfDaysInMonth; day++)
            {
                var recordVM = new OvertimeRecordVM(day, this);

                OvertimeRecord? record = records.Find(i => i.Date.Day == day);
                if (record != null)
                {
                    recordVM.NumOfHours = record.NumOfHours;
                }

                OvertimeRecordVMs.Add(recordVM);
            }
            CalculateTotalOvertimePay();
        }

        private async Task SaveOvertimeRecords()
        {
            List<OvertimeRecord> records = OvertimeRecordVMs
                .Where(i => i.NumOfHours != 0)
                .Select(i => new OvertimeRecord()
                {
                    EmployeeId = currentEmployee.Id,
                    Date = new DateTime(CurrentMonthYear.Year, CurrentMonthYear.Month, i.Day),
                    NumOfHours = i.NumOfHours,
                })
                .ToList();
            await overtimeRepo.UpdateOvertimeRecords(CurrentEmployee.Id, records);

            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Overtime);
        }
    }
}