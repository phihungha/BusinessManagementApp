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
            = App.Current.ServiceProvider.GetRequiredService<OverviewVM>();
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
                CurrentViewVM = GetViewModelFromViewName(value);
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
            ObservableObject? viewModel = GetViewModelFromViewName(content.TargetViewName);
            if (content.Extra != null && viewModel != null)
                PassExtraToViewModel(viewModel, content.Extra);
            CurrentViewVM = viewModel;
        }

        private ObservableObject? GetViewModelFromViewName(WorkspaceViewName targetViewName)
        {
            var serviceProvider = App.Current.ServiceProvider;
            switch (targetViewName)
            {
                case WorkspaceViewName.Overview:
                    return serviceProvider.GetRequiredService<OverviewVM>();
                case WorkspaceViewName.Orders:
                    return serviceProvider.GetRequiredService<OrdersVM>();
                case WorkspaceViewName.EmployeeInfo:
                    return serviceProvider.GetRequiredService<EmployeeInfoVM>();
                case WorkspaceViewName.EmployeeInfoDetails:
                    return serviceProvider.GetRequiredService<EmployeeInfoDetailsVM>();
                default:
                    return null;
            }
        }

        private void PassExtraToViewModel(ObservableObject viewModel, object id)
        {
            var detailsViewModel = (DetailsObservableValidator)viewModel;
            detailsViewModel?.LoadDataFromId(id);
        }
    }
}
