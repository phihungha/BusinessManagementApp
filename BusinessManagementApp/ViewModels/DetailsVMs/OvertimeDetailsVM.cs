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
        private class Data
        {
            public int StartHour { get; set; }
            public int EndHour { get; set; }
        }

        private Data oldData;
        private Data data;

        public int Day { get; }

        public bool IsEnabled { get; set; } = false;

        public int StartHour
        {
            get => data.StartHour;
            set
            {
                SetProperty(data.StartHour, value, data, (model, val) => model.StartHour = val);
                NumberOfHours = EndHour - StartHour;
            }
        }

        public int EndHour
        {
            get => data.EndHour;
            set
            {
                SetProperty(data.EndHour, value, data, (model, val) => model.EndHour = val);
                NumberOfHours = EndHour - StartHour;
            }
        }

        private int numberOfHours;

        public int NumberOfHours
        {
            get => numberOfHours;
            set => SetProperty(ref numberOfHours, value);
        }

        public int MinOvertimeHour { get; }
        public int MaxOvertimeHour { get; }

        private bool isEditing = false;

        public OvertimeRecordVM(int day, int minOvertimeHour, int maxOvertimeHour)
        {
            Day = day;
            MinOvertimeHour = minOvertimeHour;
            MaxOvertimeHour = maxOvertimeHour;

            data = new Data()
            {
                StartHour = 17,
                EndHour = 18
            };
            oldData = data;
            NumberOfHours = EndHour - StartHour;
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

        private int minOvertimeHour = 17;

        private int maxOvertimeHour = 20;

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

            minOvertimeHour = configRepo.Config.MinOvertimeHour;
            maxOvertimeHour = configRepo.Config.MaxOvertimeHour;

            Save = new AsyncRelayCommand(SaveOvertimeRecords);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillRating)
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

        private void LoadOvertimeRecordVMs(List<OvertimeRecord> records)
        {
            int numberOfDaysInMonth = DateTime.DaysInMonth(CurrentMonthYear.Year, CurrentMonthYear.Month);
            for (int day = 1; day < numberOfDaysInMonth; day++)
            {
                var recordVM = new OvertimeRecordVM(day, minOvertimeHour, maxOvertimeHour);

                OvertimeRecord? record = records.Find(i => i.Date.Day == day);
                if (record != null)
                {
                    recordVM.IsEnabled = true;
                    recordVM.StartHour = record.StartHour;
                    recordVM.EndHour = record.EndHour;
                }

                OvertimeRecordVMs.Add(recordVM);
            }
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