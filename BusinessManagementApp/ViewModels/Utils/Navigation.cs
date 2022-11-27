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
        /// Navigate to a view and send its view model an object.
        /// Used for item details/edit views that need the ID of an item.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        /// <param name="extra">Object to send to the view model</param>
        public static void NavigateToWithExtra(WorkspaceViewName viewName, object extra)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavigationMessage(viewName, extra));
        }
    }

    /// <summary>
    /// Methods to navigate to a view on the main window.
    /// </summary>
    public class MainWindowNavUtils
    {
        /// <summary>
        /// Navigate to a view.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        public static void NavigateTo(MainWindowViewName viewName)
        {
            WeakReferenceMessenger.Default.Send(new MainWindowNavigationMessage(viewName));
        }
    }
}
