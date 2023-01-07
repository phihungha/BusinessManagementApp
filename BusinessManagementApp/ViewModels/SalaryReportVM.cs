using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public ICommand Search { get; }

        private int selectedmonth = DateTime.Now.Month;

        public int SelectedMonth
        {
            get => selectedmonth;
            set => SetProperty(ref selectedmonth, value, true);
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

            Search = new RelayCommand(() => SalaryRecordView.Refresh());

            LoadData();
        }

        private bool FilterList(object item)
        {
            var salaryrecord = (SalaryRecord)item;
            switch (SearchBy)
            {
                case SalaryInfoSearchBy.Department:
                    return salaryrecord.Employee.Department.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.Name:
                    return salaryrecord.Employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.Id:
                    return salaryrecord.Employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                default:
                    throw new ArgumentException(nameof(item));
            }
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            salaries.AddRange(await salaryRecordsRepo.GetSalaryRecords(SelectedYear, SelectedMonth));
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}