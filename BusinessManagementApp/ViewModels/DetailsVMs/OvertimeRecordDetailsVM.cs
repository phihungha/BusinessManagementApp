using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{

    public class OvertimeDetailsVM : ViewModelBase
    {
        #region Dependencies

        private OvertimeRecordsRepo overtimeRecordsRepo;

        #endregion Dependencies

        private Employee currentEmployee = new();

        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set => SetProperty(ref currentEmployee, value);
        }


    }
}
