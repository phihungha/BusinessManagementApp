using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BusinessManagementApp.ViewModels.Navigation
{
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
}