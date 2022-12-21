using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
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
        private OvertimeRecord oldOvertimeRecord;
        public OvertimeRecord OvertimeRecord { get; private set; }

        public int Day
        {
            get => OvertimeRecord.Date.Day;
            set => SetProperty(OvertimeRecord.Date.Day, value, OvertimeRecord,
                               (model, name) => model.Date = new DateTime(name, model.Date.Month, model.Date.Year));
        }

        public int StartHour
        {
            get => OvertimeRecord.StartHour;
            set
            {
                SetProperty(OvertimeRecord.StartHour, value, OvertimeRecord,
                            (model, val) => model.StartHour = val);
                NumberOfHours = EndHour - StartHour;
            }
        }

        public int EndHour
        {
            get => OvertimeRecord.EndHour;
            set
            {
                SetProperty(OvertimeRecord.EndHour, value, OvertimeRecord,
                            (model, val) => model.EndHour = val);
                NumberOfHours = EndHour - StartHour;
            }
        }

        private int numberOfHours;

        public int NumberOfHours
        {
            get => numberOfHours;
            set => SetProperty(ref numberOfHours, value);
        }

        private bool isEditing = false;

        public OvertimeRecordVM()
        {
            OvertimeRecord = new OvertimeRecord()
            {
                Date = DateTime.Now.Date,
                StartHour = 17,
                EndHour = 18
            };
            oldOvertimeRecord = OvertimeRecord;
            NumberOfHours = EndHour - StartHour;
        }

        public OvertimeRecordVM(OvertimeRecord record)
        {
            OvertimeRecord = record;
            oldOvertimeRecord = OvertimeRecord;
            NumberOfHours = EndHour - StartHour;
        }

        public void BeginEdit()
        {
            if (!isEditing)
            {
                oldOvertimeRecord = OvertimeRecord;
                isEditing = true;
            }
        }

        public void CancelEdit()
        {
            if (isEditing)
            {
                OvertimeRecord = oldOvertimeRecord;
                isEditing = false;
            }
        }

        public void EndEdit()
        {
            if (isEditing)
            {
                oldOvertimeRecord = OvertimeRecord;
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

        public OvertimeDetailsVM(OvertimeRepo overtimeRepo)
        {
            this.overtimeRepo = overtimeRepo;

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
            LoadOvertimeRecordVMsFromModels(overview.Records);
            CurrentEmployee = overview.Employee;
            CurrentMonthYear = parameter.MonthYear;

            CanSave = true;
        }

        private void LoadOvertimeRecordVMsFromModels(List<OvertimeRecord> records)
        {
            List<OvertimeRecordVM> viewModels = records
                .Select(i => new OvertimeRecordVM(i))
                .ToList();
            OvertimeRecordVMs.AddRange(viewModels);
        }

        private async Task SaveOvertimeRecords()
        {
            List<OvertimeRecord> records = OvertimeRecordVMs
                .Select(i => i.OvertimeRecord)
                .ToList();
            await overtimeRepo.UpdateOvertimeRecords(CurrentEmployee.Id, records);

            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Overtime);
        }
    }
}