using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManagementApp.ViewModels
{
    public enum MainWindowViewName
    {
        Workspace,
        Login
    }

    public class MainWindowVM : ObservableObject
    {
        private ObservableObject currentViewVM
            = App.Current.ServiceProvider.GetRequiredService<LoginVM>();

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
                CurrentViewVM = App.Current.ServiceProvider.GetRequiredService<WorkspaceVM>();
            }
            else
            {
                CurrentViewVM = App.Current.ServiceProvider.GetRequiredService<LoginVM>();
            }
        }
    }
}