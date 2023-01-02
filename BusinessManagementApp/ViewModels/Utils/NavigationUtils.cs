using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Messaging;

namespace BusinessManagementApp.ViewModels.Utils
{
    /// <summary>
    /// Methods to navigate to a view on the workspace area.
    /// </summary>
    public class WorkspaceNavUtils
    {
        /// <summary>
        /// Navigate to a view.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        public static void NavigateTo(WorkspaceViewName viewName)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavMessage(viewName, false));
        }

        /// <summary>
        /// Navigate to a view and send its view model an object.
        /// Used for item details/edit views that need the ID of an item.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        /// <param name="extra">Object to send to the view model</param>
        public static void NavigateToWithExtra(WorkspaceViewName viewName, object extra)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavMessage(viewName, false, extra));
        }

        /// <summary>
        /// Navigate to a view, send its view model an object,
        /// and save current view on the backstack.
        /// </summary>
        /// <param name="viewName">Name of view to go to</param>
        /// <param name="extra">Object to send to the view model</param>
        public static void NavigateToWithExtraAndBackstack(WorkspaceViewName viewName, object extra)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceNavMessage(viewName, true, extra));
        }

        /// <summary>
        /// Navigate back to the previous view.
        /// </summary>
        public static void NavigateBack()
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceBackNavMessage());
        }

        /// <summary>
        /// Navigate back to the previous view and send it an object.
        /// Used for item selection views that returns selected items to the previous view.
        /// </summary>
        /// <param name="extra">Object to send to the view model</param>
        public static void NavigateBackWithExtra(object extra)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceBackNavMessage(extra));
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