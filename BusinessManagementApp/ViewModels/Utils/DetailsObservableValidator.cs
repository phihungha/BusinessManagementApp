using CommunityToolkit.Mvvm.ComponentModel;

namespace BusinessManagementApp.ViewModels.Utils
{
    public abstract class DetailsObservableValidator : ObservableValidator
    {
        public abstract void LoadDataFromId(object id);
    }
}