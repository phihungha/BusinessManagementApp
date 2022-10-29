using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels
{
    public enum ViewName
    {
        Overview,
        Orders,
        EmployeeInfo
    }

    public class WorkspaceVM : ObservableObject
    {
        private ObservableObject currentViewVM = App.Current.Services.GetRequiredService<OverviewVM>();
        public ObservableObject CurrentViewVM
        {
            get => currentViewVM;
            set => SetProperty(ref currentViewVM, value);
        }

        public ViewName[] ViewNames { get; } = new[] 
        {   
            ViewName.Overview, 
            ViewName.Orders, 
            ViewName.EmployeeInfo 
        };

        private ViewName selectedViewName = ViewName.Overview;
        public ViewName SelectedViewName
        {
            get => selectedViewName;
            set
            {
                SetProperty(ref selectedViewName, value);
                ChangeView();
            }
        }

        private void ChangeView()
        {
            switch (SelectedViewName)
            {
                case ViewName.Overview:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OverviewVM>();
                    break;
                case ViewName.Orders:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OrdersVM>();
                    break;
                case ViewName.EmployeeInfo:
                    CurrentViewVM = App.Current.Services.GetRequiredService<EmployeeInfoVM>();
                    break;
            }
        }
    }
}
