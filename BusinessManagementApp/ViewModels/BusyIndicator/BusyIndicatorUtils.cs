using CommunityToolkit.Mvvm.Messaging;

namespace BusinessManagementApp.ViewModels.BusyIndicator
{
    public static class BusyIndicatorUtils
    {
        /// <summary>
        /// Enable/disable the workspace busy indicator.
        /// </summary>
        /// <param name="isEnabled">True if want to enable</param>
        public static void SetBusyIndicator(bool isEnabled)
        {
            WeakReferenceMessenger.Default.Send(new SetBusyIndicatorMessage(isEnabled));
        }
    }
}