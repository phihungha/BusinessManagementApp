using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BusinessManagementApp.ViewModels.Navigation
{
    /// <summary>
    /// Message to indicate a navigation request to return to the previous view.
    /// </summary>
    public class WorkspaceBackNavMessage : ValueChangedMessage<NavigationMessageContent>
    {
        public WorkspaceBackNavMessage(object? extra = null)
            : base(new NavigationMessageContent(null, false, extra))
        {
        }
    }
}