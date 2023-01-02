using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BusinessManagementApp.ViewModels.BusyIndicator
{
    /// <summary>
    /// Message to enable/disable the busy indicator.
    /// </summary>
    public class SetBusyIndicatorMessage : ValueChangedMessage<bool>
    {
        public SetBusyIndicatorMessage(bool value) : base(value)
        {
        }
    }
}