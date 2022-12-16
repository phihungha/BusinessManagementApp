using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BusinessManagementApp.Utils;
using System.Reactive.Linq;

namespace BusinessManagementApp.ViewModels
{
    public enum SalaryInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Base Salary")]
        BaseSalary,

        [Description("Supplement Salary")]
        SupplementSalary,

        [Description("Bonus Salary")]
        BonusSalary,

        [Description("Total Salary")]
        TotalSalary,

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

        private decimal selectedmonth = 1;

        public decimal SelectedMonth
        {
            get => selectedmonth;
            set => SetProperty(ref selectedmonth, value, true);
        }

        private decimal selectedyear = 1;

        public decimal SelectedYear
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

                case SalaryInfoSearchBy.BaseSalary:
                    return salaryrecord.BaseSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.SupplementSalary:
                    return salaryrecord.BaseSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.BonusSalary:
                    return salaryrecord.BonusSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                case SalaryInfoSearchBy.TotalSalary:
                    return salaryrecord.TotalSalary.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;

                default:
                    return salaryrecord.Month == SelectedMonth && salaryrecord.Year == SelectedYear;
            }
        }

        private async void LoadData()
        {
            salaries.AddRange(await salaryRecordsRepo.GetSalaryRecords());
        }
    }
}
