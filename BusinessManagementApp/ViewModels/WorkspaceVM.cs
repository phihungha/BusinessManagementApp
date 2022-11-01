using BusinessManagementApp.ViewModels.EditVMs;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManagementApp.ViewModels
{
    public enum WorkspaceViewName
    {
        Overview,
        Orders,
        EmployeeInfo,
        EmployeeInfoDetails
    }

    /// <summary>
    /// Contains the content of a navigation message 
    /// including name of view to go to and extra message
    /// </summary>
    public struct NavigationMessageContent
    {
        public WorkspaceViewName TargetViewName { get; set; }
        public object? Extra { get; set; }

        /// <summary>
        /// Contains the content of a navigation message 
        /// including name of view to go to and extra message
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public NavigationMessageContent(WorkspaceViewName targetViewName, object? extra)
        {
            TargetViewName = targetViewName;
            Extra = extra;
        }
    }

    /// <summary>
    /// Message to indicate a navigation request to another view on the workspace view.
    /// </summary>
    public class WorkspaceNavigationMessage : ValueChangedMessage<NavigationMessageContent>
    {
        /// <summary>
        /// Message to indicate a navigation request to another view on the workspace view.
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public WorkspaceNavigationMessage(WorkspaceViewName targetViewName, object? extra = null)
            : base(new NavigationMessageContent(targetViewName, extra))
        {
        }
    }

    public class WorkspaceVM : ObservableObject
    {
        private ObservableObject? currentViewVM 
            = App.Current.Services.GetRequiredService<OverviewVM>();
        public ObservableObject? CurrentViewVM
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
                ChangeView(value);
            }
        }

        public WorkspaceVM()
        {
            WeakReferenceMessenger
                .Default
                .Register<WorkspaceNavigationMessage>(
                    this, 
                    (r, m) => HandleNavigationMessageContent(m.Value)
                );
        }

        private void HandleNavigationMessageContent(NavigationMessageContent content)
        {
            SelectedViewName = content.TargetViewName;

            if (content.Extra == null)
                ChangeView(content.TargetViewName);
            else
                ChangeViewAndPassId(content.TargetViewName, content.Extra);
        }

        private void ChangeView(WorkspaceViewName targetViewName)
        {
            switch (targetViewName)
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
                default:
                    CurrentViewVM = null;
                    break;
            }
        }

        private void ChangeViewAndPassId(WorkspaceViewName targetViewName, object id)
        {
            DetailsObservableValidator? viewModel = null;

            switch (targetViewName)
            {
                case WorkspaceViewName.EmployeeInfoDetails:
                    viewModel = App.Current.Services.GetRequiredService<EmployeeInfoDetailsVM>();
                    break;
            }

            viewModel?.LoadDataFromId(id);

            CurrentViewVM = viewModel;
        }
    }
}
