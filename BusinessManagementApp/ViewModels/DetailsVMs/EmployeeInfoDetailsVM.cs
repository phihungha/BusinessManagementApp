using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class EmployeeInfoDetailsVM : ViewModelBase
    {
        private int id = 0;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string citizenId = string.Empty;

        [CitizenId]
        public string CitizenId
        {
            get => citizenId;
            set => SetProperty(ref citizenId, value, true);
        }

        private string email = string.Empty;

        [EmailAddress]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        private string phoneNumber = string.Empty;

        [PhoneNumber]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        public ICommand Cancel { get; private set; }

        public EmployeeInfoDetailsVM()
        {
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo)
                );
        }

        public override async void ReceiveExtra(object id)
        {
            await Task.Delay(500);
            Id = (int)id;
        }
    }
}
