using CommunityToolkit.Mvvm.Messaging;

namespace BusinessManagementApp.ViewModels.BusyIndicator
{
    /// <summary>
    /// Helper class for busy indicator management.
    /// </summary>
    public class BusyIndicatorHelper
    {
        public BusyIndicatorHelper(ISupportBusyIndicator viewModel)
        {
            WeakReferenceMessenger.Default.Register<SetBusyIndicatorMessage>(
                this, (sender, e) =>
            {
                viewModel.IsBusy = e.Value;
            });
        }
    }
}