using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
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
    public enum SalaryInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Department")]
        Department,
    }

    public class SalaryReportVM : ViewModelBase
    {
        private SalaryRecordsRepo salaryRecordsRepo;

        private ObservableCollection<SalaryRecord> salaries { get; } = new();

        public ICollectionView SalaryRecordView { get; }

        public string SearchText { get; set; } = string.Empty;

        public SalaryInfoSearchBy SearchBy { get; set; } = SalaryInfoSearchBy.Name;
        public ICommand Generate { get; }
        public ICommand Search { get; }
        public int MaxYear { get; } = DateTime.Now.Year;
        public int[] MonthSelections { get; } = Enumerable.Range(1, 12).ToArray();

        private int month = DateTime.Now.Month;

        public int Month
        {
            get => month;
            set => SetProperty(ref month, value);
        }

        private int selectedyear = DateTime.Now.Year;

        public int SelectedYear
        {
            get => selectedyear;
            set => SetProperty(ref selectedyear, value, true);
        }

        public SalaryReportVM(SalaryRecordsRepo salaryRecordsRepo)
        {
            this.salaryRecordsRepo = salaryRecordsRepo;

            var collectionViewSource = new CollectionViewSource() { Source = salaries };
            SalaryRecordView = collectionViewSource.View;
            SalaryRecordView.Filter = FilterList;
            Generate = new RelayCommand(LoadData);
            Search = new RelayCommand(() => SalaryRecordView.Refresh());

            LoadData();
        }

        private bool FilterList(object item)
        {
            var salaryrecord = (SalaryRecord)item;
            switch (SearchBy)
            {
                case SalaryInfoSearchBy.Department:
                    return salaryrecord.Employee.Department.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.Name:
                    return salaryrecord.Employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.Id:
                    return salaryrecord.Employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.BaseSalary:
                    return salaryrecord.BaseSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.SupplementSalary:
                    return salaryrecord.BaseSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.BonusSalary:
                    return salaryrecord.BonusSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.TotalSalary:
                    return salaryrecord.TotalSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;

                default:
                    return salaryrecord.Month == Month && salaryrecord.Year == SelectedYear;
            }
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            salaries.ClearAndAddRange(await salaryRecordsRepo.GetSalaryRecords());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}