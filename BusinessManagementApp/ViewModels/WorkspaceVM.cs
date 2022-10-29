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
    public enum SidebarNavPageNames
    {
        [Description("Overview")]
        Overview,
        [Description("Orders")]
        Orders,
        [Description("Employee info")]
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

        private SidebarNavPageNames selectedViewName = SidebarNavPageNames.EmployeeInfo;
        public SidebarNavPageNames SelectedViewName
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
                case SidebarNavPageNames.Overview:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OverviewVM>();
                    break;
                case SidebarNavPageNames.Orders:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OrdersVM>();
                    break;
                case SidebarNavPageNames.EmployeeInfo:
                    CurrentViewVM = App.Current.Services.GetRequiredService<EmployeeInfoVM>();
                    break;
            }
        }
    }
}
