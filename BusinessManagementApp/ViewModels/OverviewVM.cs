using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
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
using System.Reactive.Linq;
using System.Timers;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class OverviewVM : ViewModelBase
    {
        private OverviewRepo overviewRepo;

        public ObservableCollection<Order> PendingOrders { get; } = new();

        private ObservableValue numOfPendingOrders = new() { Value = 0 };
        public IEnumerable<ISeries> NumOfPendingOrders { get; }

        private ObservableValue totalStock = new() { Value = 0 };
        public IEnumerable<ISeries> TotalStock { get; }

        private ObservableValue todayRevenue = new() { Value = 0 };
        public IEnumerable<ISeries> TodayRevenue { get; }

        private Timer currentTimeTimer = new();

        private DateTime currentTime = DateTime.Now;

        public DateTime CurrentTime
        {
            get => currentTime;
            set => SetProperty(ref currentTime, value);
        }

        public ICommand NewOrder { get; }

        public OverviewVM(OverviewRepo overviewRepo)
        {
            this.overviewRepo = overviewRepo;

            NewOrder = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.OrderDetails));

            currentTimeTimer.Interval = 1;
            currentTimeTimer.Elapsed += CurrentTimeTimer_Elapsed;
            currentTimeTimer.Enabled = true;

            NumOfPendingOrders = new GaugeBuilder()
                .WithInnerRadius(75)
                .WithBackgroundInnerRadius(75)
                .WithLabelsSize(50)
                .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
                .AddValue(numOfPendingOrders, "Number of pending orders", SKColors.Orange, SKColors.Orange)
                .BuildSeries();

            TotalStock = new GaugeBuilder()
                .WithInnerRadius(75)
                .WithBackgroundInnerRadius(75)
                .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
                .WithLabelsSize(50)
                .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
                .AddValue(totalStock, "Stock of all products", SKColors.Green, SKColors.Green)
                .BuildSeries();

            TodayRevenue = new GaugeBuilder()
                .WithInnerRadius(75)
                .WithBackgroundInnerRadius(75)
                .WithBackground(new SolidColorPaint(new SKColor(100, 181, 246, 90)))
                .WithLabelsSize(20)
                .WithLabelFormatter(i => i.PrimaryValue.ToString("c"))
                .WithLabelsPosition(PolarLabelsPosition.ChartCenter)
                .AddValue(todayRevenue, "Today's revenue", SKColors.Red, SKColors.Red)
                .BuildSeries();

            LoadData();
        }

        private void CurrentTimeTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);

            miOverview overview = await overviewRepo.GetOverview();
            PendingOrders.AddRange(overview.PendingOrders);
            numOfPendingOrders.Value = overview.NumOfPendingOrders;
            totalStock.Value = overview.TotalStock;
            todayRevenue.Value = overview.TodayRevenue;

            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}