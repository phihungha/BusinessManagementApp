using CommunityToolkit.Mvvm.Messaging;

namespace BusinessManagementApp.ViewModels.Utils
{
    /// <summary>
    /// Methods to navigate to a view on the workspace view.
    /// </summary>
    public class WorkspaceNavUtils
    {
        /// <summary>
        /// Navigate to a view.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        public static void NavigateTo(WorkspaceViewName viewName)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavigationMessage(viewName));
        }

        /// <summary>
        /// Navigate to a view and pass it an ID.
        /// Used for item details/edit views that need the ID of an item.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        /// <param name="id">ID value</param>
        public static void NavigateToWithId(WorkspaceViewName viewName, int id)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavigationMessage(viewName, id));
        }
    }
}
