using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels.Utils
{
    public abstract class DetailsObservableValidator : ObservableValidator
    {
        public abstract void LoadDataFromId(object id);
    }
}
