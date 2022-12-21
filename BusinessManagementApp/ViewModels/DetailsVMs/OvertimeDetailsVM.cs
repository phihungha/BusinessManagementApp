using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
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
        private class Data
        {
            public int StartHour { get; set; }
            public int EndHour { get; set; }
        }

        private Data oldData;
        private Data data;

        private OvertimeDetailsVM parentVM;

        private decimal OvertimeHourlyRate => parentVM.OvertimeHourlyRate;
        public int MinOvertimeHour => parentVM.MinOvertimeHour;
        public int MaxOvertimeHour => parentVM.MaxOvertimeHour;

        public int Day { get; }

        private bool isEnabled = false;

        public bool IsEnabled
        {
            get => isEnabled;
            set 
            {
                SetProperty(ref isEnabled, value);
                if (value)
                {
                    CalculateOvertimePay(NumOfHours);
                }
                else
                {
                    OvertimePay = 0;
                }
            } 
        }

        public int StartHour
        {
            get => data.StartHour;
            set
            {
                SetProperty(data.StartHour, value, data, (model, val) => model.StartHour = val);
                CalculateNumOfHours(value, EndHour);
            }
        }

        public int EndHour
        {
            get => data.EndHour;
            set
            {
                SetProperty(data.EndHour, value, data, (model, val) => model.EndHour = val);
                CalculateNumOfHours(StartHour, value);
            }
        }

        private int numOfHours;

        public int NumOfHours
        {
            get => numOfHours;
            set
            {
                SetProperty(ref numOfHours, value);
                if (IsEnabled)
                {
                    CalculateOvertimePay(value);
                }
            }
        }

        private decimal overtimePay;

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

            data = new Data()
            {
                StartHour = MinOvertimeHour,
                EndHour = MaxOvertimeHour
            };
            oldData = data;
            NumOfHours = EndHour - StartHour;
            OvertimePay = 0;
        }

        private void CalculateOvertimePay(int numOfHours)
        {
            OvertimePay = OvertimeHourlyRate * numOfHours;
        }

        private void CalculateNumOfHours(int startHour, int endHour)
        {
            NumOfHours = EndHour - StartHour;
        }

        public void BeginEdit()
        {
            if (!isEditing)
            {
                oldData = data;
                isEditing = true;
            }
        }

        public void CancelEdit()
        {
            if (isEditing)
            {
                data = oldData;
                isEditing = false;
            }
        }

        public void EndEdit()
        {
            if (isEditing)
            {
                oldData = data;
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

        private decimal totalOvertimePay = -1;

        public decimal TotalOvertimePay
        {
            get => totalOvertimePay;
            set => SetProperty(ref totalOvertimePay, value);
        }

        public int MinOvertimeHour = -1;
        public int MaxOvertimeHour = -1;
        public decimal OvertimeHourlyRate = -1;

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

            MinOvertimeHour = configRepo.Config.MinOvertimeHour;
            MaxOvertimeHour = configRepo.Config.MaxOvertimeHour;
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
                    recordVM.IsEnabled = true;
                    recordVM.StartHour = record.StartHour;
                    recordVM.EndHour = record.EndHour;
                }

                OvertimeRecordVMs.Add(recordVM);
            }
            CalculateTotalOvertimePay();
        }

        private async Task SaveOvertimeRecords()
        {
            List<OvertimeRecord> records = OvertimeRecordVMs
                .Where(i => i.IsEnabled)
                .Select(i => new OvertimeRecord()
                {
                    EmployeeId = currentEmployee.Id,
                    Date = new DateTime(CurrentMonthYear.Year, CurrentMonthYear.Month, i.Day),
                    StartHour = i.StartHour,
                    EndHour = i.EndHour
                })
                .ToList();
            await overtimeRepo.UpdateOvertimeRecords(CurrentEmployee.Id, records);

            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Overtime);
        }
    }
}