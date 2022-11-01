using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.EditVMs
{
    public class EmployeeInfoDetailsVM : DetailsObservableValidator
    {
        private int id = 0;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public ICommand Cancel { get; private set; }

        public EmployeeInfoDetailsVM()
        {
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo)
                );
        }

        public override void LoadDataFromId(object id)
        {
            Id = (int)id;
        }
    }
}
