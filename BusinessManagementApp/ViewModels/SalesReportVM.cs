using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
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
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class SalesReportVM : ViewModelBase
    {
        private const double GaugeInnerRadius = 75;
        private const double GaugeBackgroundInnerRadius = 75;
        private const PolarLabelsPosition GaugeLabelPosition = PolarLabelsPosition.ChartCenter;

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
        public IEnumerable<ISeries> ProductCategoryStats { get; private set; }
        public SolidColorPaint ProductCategoryLegendBackgroundPaint { get; } = new(new SKColor(0, 0, 0));

        private ObservableCollection<ProductStats> productStats = new();
        public IEnumerable<ISeries> ProductStats { get; }

        private ObservableCollection<CustomerStats> customerStats = new();
        public IEnumerable<ISeries> CustomerStats { get; }

        private ObservableCollection<EmployeeStats> employeeStats = new();
        public IEnumerable<ISeries> EmployeeStats { get; }

        #endregion Stats

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

        public SalesReportVM(SalesReportRepo salesReportRepo)
        {
            this.salesReportRepo = salesReportRepo;

            Generate = new RelayCommand(LoadData);

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

            LoadData();
        }

        private async void LoadData()
        {
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