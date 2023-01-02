using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BusinessManagementApp.ViewModels.Navigation
{
    /// <summary>
    /// Message to indicate a navigation request to another view on the workspace view.
    /// </summary>
    public class WorkspaceNavMessage : ValueChangedMessage<NavigationMessageContent>
    {
        /// <summary>
        /// Message to indicate a navigation request to another view on the workspace view.
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public WorkspaceNavMessage(WorkspaceViewName targetViewName, bool saveOnBackstack, object? extra = null)
            : base(new NavigationMessageContent(targetViewName, saveOnBackstack, extra))
        {
        }
    }
}