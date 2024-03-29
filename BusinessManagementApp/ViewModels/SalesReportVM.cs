﻿using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum SalesReprotSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name
    }

    public class SalesReportVM : ViewModelBase
    {
        private const double GaugeInnerRadius = 75;
        private const double GaugeBackgroundInnerRadius = 75;
        private const PolarLabelsPosition GaugeLabelPosition = PolarLabelsPosition.ChartCenter;

        private int currentTabIndex = 0;

        public int CurrentTabIndex
        {
            get => currentTabIndex;
            set
            {
                SetProperty(ref currentTabIndex, value);
                SearchText = "";
                SearchBy = SalesReprotSearchBy.Name;
            }
        }

        #region Dependencies

        private SalesReportRepo salesReportRepo;

        #endregion Dependencies

        #region Stats

        private ObservableCollection<decimal> revenueByDay = new();
        public IEnumerable<ISeries> RevenueByDay { get; }
        public IEnumerable<Axis> RevenueByDayXAxis { get; }
        public IEnumerable<Axis> RevenueByDayYAxis { get; }

        private ObservableValue totalRevenue = new() { Value = 0 };
        public IEnumerable<ISeries> TotalRevenue { get; }

        private ObservableValue avgRevenue = new() { Value = 0 };
        public IEnumerable<ISeries> AvgRevenue { get; }

        private ObservableValue avgNumOfOrdersPerEmployee = new() { Value = 0 };
        public IEnumerable<ISeries> AvgNumOfOrdersPerEmployee { get; }

        private ObservableValue numOfOrdersCompleted = new() { Value = 0 };
        private ObservableValue numOfOrdersCanceled = new() { Value = 0 };
        private ObservableValue numOfOrdersReturned = new() { Value = 0 };
        private ObservableValue numOfOrdersMade = new() { Value = 0 };
        public IEnumerable<ISeries> OrderCounts { get; }

        public ObservableCollection<ProductCategoryStats> ProductCategoryStatsCollection { get; } = new();
        public IEnumerable<ISeries>? ProductCategoryStats { get; private set; }

        public ICollectionView ProductsView { get; }
        private ObservableCollection<ProductStats> productStats { get; } = new();

        public ICollectionView CustomersView { get; }
        private ObservableCollection<CustomerStats> customerStats { get; } = new();

        public ICollectionView EmployeesView { get; }
        private ObservableCollection<EmployeeStats> employeeStats { get; } = new();

        #endregion Stats

        #region Searching

        public string SearchText { get; set; } = string.Empty;

        public SalesReprotSearchBy SearchBy { get; set; } = SalesReprotSearchBy.Name;

        #endregion Searching

        #region Time selection

        public int[] MonthSelections { get; } = Enumerable.Range(1, 12).ToArray();

        private int month = DateTime.Now.Month;

        public int Month
        {
            get => month;
            set => SetProperty(ref month, value);
        }

        private int year = DateTime.Now.Year;

        public int Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        public int MaxYear { get; } = DateTime.Now.Year;

        #endregion Time selection

        public ICommand Generate { get; }
        public ICommand Search { get; }

        public SalesReportVM(SalesReportRepo salesReportRepo)
        {
            this.salesReportRepo = salesReportRepo;

            var collectionViewSource = new CollectionViewSource() { Source = productStats };
            ProductsView = collectionViewSource.View;
            ProductsView.Filter = ProductsViewFilter;

            collectionViewSource = new CollectionViewSource() { Source = customerStats };
            CustomersView = collectionViewSource.View;
            CustomersView.Filter = CustomersViewFilter;

            collectionViewSource = new CollectionViewSource() { Source = employeeStats };
            EmployeesView = collectionViewSource.View;
            EmployeesView.Filter = EmployeesViewFilter;

            RevenueByDay = new ISeries[]
            {
                new LineSeries<decimal>
                {
                    Values = revenueByDay,
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 }
                }
            };

            RevenueByDayXAxis = new Axis[]
            {
                new Axis()
                {
                    Labels = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month))
                                .Select(i => i.ToString())
                                .ToArray()
                }
            };

            RevenueByDayYAxis = new Axis[]
            {
                new Axis()
                {
                    Labeler = Labelers.Currency
                }
            };

            TotalRevenue = new GaugeBuilder()
                .WithInnerRadius(GaugeInnerRadius)
                .WithBackgroundInnerRadius(GaugeBackgroundInnerRadius)
                .WithLabelsSize(18)
                .WithLabelsPosition(GaugeLabelPosition)
                .WithLabelFormatter(i => i.PrimaryValue.ToString("c"))
                .AddValue(totalRevenue, "Total revenue", SKColors.Red, SKColors.Red)
                .BuildSeries();

            AvgRevenue = new GaugeBuilder()
                .WithInnerRadius(GaugeInnerRadius)
                .WithBackgroundInnerRadius(GaugeBackgroundInnerRadius)
                .WithLabelsSize(20)
                .WithLabelsPosition(GaugeLabelPosition)
                .WithLabelFormatter(i => i.PrimaryValue.ToString("c"))
                .AddValue(avgRevenue, "Average revenue", SKColors.Green, SKColors.Green)
                .BuildSeries();

            AvgNumOfOrdersPerEmployee = new GaugeBuilder()
                .WithInnerRadius(GaugeInnerRadius)
                .WithBackgroundInnerRadius(GaugeBackgroundInnerRadius)
                .WithLabelsSize(35)
                .WithLabelsPosition(GaugeLabelPosition)
                .AddValue(avgNumOfOrdersPerEmployee, "Average # of orders per employee",
                          SKColors.Orange, SKColors.Orange)
                .BuildSeries();

            OrderCounts = new GaugeBuilder()
                .WithInnerRadius(20)
                .WithBackgroundInnerRadius(16)
                .WithLabelsSize(18)
                .WithLabelsPosition(PolarLabelsPosition.Start)
                .WithLabelFormatter(point => point.PrimaryValue + " " + point.Context.Series.Name)
                .AddValue(numOfOrdersCompleted, "completed",
                          SKColors.Green, SKColors.Green)
                .AddValue(numOfOrdersReturned, "returned",
                          SKColors.Orange, SKColors.Orange)
                .AddValue(numOfOrdersCanceled, "canceled",
                          SKColors.Red, SKColors.Red)
                .AddValue(numOfOrdersMade, "total",
                          SKColors.Gray, SKColors.Gray)
                .BuildSeries();

            Generate = new RelayCommand(LoadData);
            Search = new RelayCommand(ExecuteSearch);

            LoadData();
        }

        private void ExecuteSearch()
        {
            switch (CurrentTabIndex)
            {
                case 1:
                    ProductsView.Refresh();
                    break;

                case 2:
                    CustomersView.Refresh();
                    break;

                case 3:
                    EmployeesView.Refresh();
                    break;
            }
        }

        private bool ProductsViewFilter(object item)
        {
            var product = ((ProductStats)item).Product;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SalesReprotSearchBy.Name:
                    return product.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SalesReprotSearchBy.Id:
                    return product.Id.ToString() == SearchText;

                default:
                    return false;
            }
        }

        private bool CustomersViewFilter(object item)
        {
            var customer = ((CustomerStats)item).Customer;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SalesReprotSearchBy.Name:
                    return customer.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SalesReprotSearchBy.Id:
                    return customer.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private bool EmployeesViewFilter(object item)
        {
            var employee = ((EmployeeStats)item).Employee;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SalesReprotSearchBy.Name:
                    return employee.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SalesReprotSearchBy.Id:
                    return employee.Id.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            SalesStats stats = await salesReportRepo.GetSalesReport(Year, Month);
            revenueByDay.ClearAndAddRange(stats.RevenueByDay);
            totalRevenue.Value = (double)stats.TotalRevenue;
            avgRevenue.Value = (double)stats.AvgRevenue;
            avgNumOfOrdersPerEmployee.Value = stats.AvgNumOfOrdersPerEmployee;
            numOfOrdersCompleted.Value = stats.NumOfOrdersCompleted;
            numOfOrdersReturned.Value = stats.NumOfOrdersReturned;
            numOfOrdersCanceled.Value = stats.NumOfOrdersCanceled;
            numOfOrdersMade.Value = stats.NumOfOrdersMade;
            ProductCategoryStatsCollection.ClearAndAddRange(stats.ProductCategoryStats);
            productStats.ClearAndAddRange(stats.ProductStats);
            employeeStats.ClearAndAddRange(stats.EmployeeStats);
            customerStats.ClearAndAddRange(stats.CustomerStats);
            LoadProductCategoryPieChart();
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private void LoadProductCategoryPieChart()
        {
            ProductCategoryStats = ProductCategoryStatsCollection.Select(
                i => new PieSeries<int>
                {
                    Name = i.Category.Name,
                    Values = new int[] { i.QuantitySold }
                }
                ).ToArray();
        }
    }
}