using CommunityToolkit.Mvvm.Messaging;

namespace BusinessManagementApp.ViewModels.BusyIndicator
{
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