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
    public class EmployeeInfoEditVM : ObservableObject
    {
        public ICommand Cancel { get; private set; }

        public EmployeeInfoEditVM()
        {
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.ChangeView(WorkspaceViewName.EmployeeInfo)
                );
        }
    }
}
