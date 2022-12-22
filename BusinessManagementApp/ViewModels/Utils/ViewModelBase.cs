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
        /// Begin loading data for the view model.
        /// Can accept an extra object (e.g. ID of a selected item)
        /// </summary>
        /// <param name="extra">Extra object</param>
        public virtual void LoadData(object? extra = null)
        { 
        }

        /// <summary>
        /// Called when returned from another view.
        /// Can accept an extra object returned from another view.
        /// </summary>
        /// <param name="extra">Extra object</param>
        public virtual void OnBack(object? extra = null)
        {
        }
    }
}