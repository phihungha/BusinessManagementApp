using BusinessManagementApp.ViewModels.EditVMs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels
{
    public enum WorkspaceViewName
    {
        Overview,
        Orders,
        EmployeeInfo,
        EmployeeInfoEdit
    }

    public class WorkspaceViewChangeMessage : ValueChangedMessage<WorkspaceViewName>
    {
        public WorkspaceViewChangeMessage(WorkspaceViewName viewName) : base(viewName)
        {
        }
    }

    public class WorkspaceVM : ObservableObject
    {
        private ObservableObject currentViewVM = App.Current.Services.GetRequiredService<OverviewVM>();
        public ObservableObject CurrentViewVM
        {
            get => currentViewVM;
            set => SetProperty(ref currentViewVM, value);
        }

        public WorkspaceViewName[] ViewNames { get; } = new[] 
        {   
            WorkspaceViewName.Overview, 
            WorkspaceViewName.Orders, 
            WorkspaceViewName.EmployeeInfo 
        };

        private WorkspaceViewName selectedViewName = WorkspaceViewName.Overview;
        public WorkspaceViewName SelectedViewName
        {
            get => selectedViewName;
            set
            {
                SetProperty(ref selectedViewName, value);
                ChangeView();
            }
        }

        public WorkspaceVM()
        {
            WeakReferenceMessenger.Default
                .Register<WorkspaceViewChangeMessage>(this, (r, m) => { SelectedViewName = m.Value; });
        }

        private void ChangeView()
        {
            switch (SelectedViewName)
            {
                case WorkspaceViewName.Overview:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OverviewVM>();
                    break;
                case WorkspaceViewName.Orders:
                    CurrentViewVM = App.Current.Services.GetRequiredService<OrdersVM>();
                    break;
                case WorkspaceViewName.EmployeeInfo:
                    CurrentViewVM = App.Current.Services.GetRequiredService<EmployeeInfoVM>();
                    break;
                case WorkspaceViewName.EmployeeInfoEdit:
                    CurrentViewVM = App.Current.Services.GetRequiredService<EmployeeInfoEditVM>();
                    break;
            }
        }
    }
}
