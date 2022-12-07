using CommunityToolkit.Mvvm.ComponentModel;

namespace BusinessManagementApp.ViewModels.Utils
{
    /// <summary>
    /// Base class for all view models. Supports validation 
    /// and receiving an extra object when navigating from another view.
    /// </summary>
    public abstract class ViewModelBase : ObservableValidator
    {
        /// <summary>
        /// Extra object from navigation will be passed 
        /// into the view model via this method.
        /// </summary>
        /// <param name="obj">Extra object</param>
        public virtual void ReceiveExtra(object obj)
        { 
        }
    }
}