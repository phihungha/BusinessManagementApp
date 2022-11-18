using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManagementApp.ViewModels
{
    public enum MainWindowViewName
    {
        Workspace,
        Login
    }

    /// <summary>
    /// Message to indicate a navigation request to another view on the main window.
    /// </summary>
    public class MainWindowNavigationMessage : ValueChangedMessage<MainWindowViewName>
    {
        /// <summary>
        /// Message to indicate a navigation request to another view on the main window.
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        public MainWindowNavigationMessage(MainWindowViewName targetViewName)
            : base(targetViewName)
        {
        }
    }

    public class MainWindowVM : ObservableObject
    {
        private ObservableObject currentViewVM = App.Current.Services.GetRequiredService<LoginVM>();
        public ObservableObject CurrentViewVM
        {
            get => currentViewVM;
            set => SetProperty(ref currentViewVM, value);
        }

        public MainWindowVM()
        {
            WeakReferenceMessenger
                .Default
                .Register<MainWindowNavigationMessage>(
                    this,
                    (r, m) => HandleNavigationMessage(m.Value)
                );
        }

        private void HandleNavigationMessage(MainWindowViewName viewName)
        {
            if (viewName == MainWindowViewName.Workspace)
            {
                CurrentViewVM = App.Current.Services.GetRequiredService<WorkspaceVM>();
            }
            else
            {
                CurrentViewVM = App.Current.Services.GetRequiredService<LoginVM>();
            }
        }
    }
}
