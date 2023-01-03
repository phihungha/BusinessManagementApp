using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace BusinessManagementApp.ViewModels
{
    public class ConfigVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private ConfigRepo configRepo = new ConfigRepo();

        #endregion Dependencies


        // Properties for inputs on the screen
        // Remember to declare validation attributes when appropriate.
        // List of validation attributes: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0
        // Check ViewModels/ValidationAttributes.cs for custom validation attributes.
        #region Input properties


        private decimal overtimeHourlyRate = 0;

        [Required]
        public decimal OvertimeHourlyRate
        {
            get => overtimeHourlyRate;
            set => SetProperty(ref overtimeHourlyRate, value);
        }
        private int maxNumOfOvertimeHours = 0;
        public int MaxNumOfOvertimeHours
        {
            get => maxNumOfOvertimeHours;
            set => SetProperty(ref maxNumOfOvertimeHours, value);
        }
        private double vATRate = 0;
        public double VATRate
        {
            get => vATRate;
            set => SetProperty(ref vATRate, value);
        }
        private decimal oldOvertimeHourlyRate = 0;
        private int oldMaxNumOfOvertimeHours = 0;
        private double oldVATRate = 0;

        #endregion Input properties

        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Reset { get; private set; }

        #endregion Commands for buttons

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public ConfigVM(ConfigRepo configRepo)
        {
            this.configRepo = configRepo;
            Save = new RelayCommand(SaveConfig);
            Reset = new RelayCommand(ResetConfig);
            LoadData();
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        private void LoadData()
        {
            OvertimeHourlyRate = configRepo.Config.OvertimeHourlyRate;
            MaxNumOfOvertimeHours = configRepo.Config.MaxNumOfOvertimeHours;
            VATRate = configRepo.Config.VATRate;
            UpdateOldValue();
        }
        private void UpdateOldValue()
        {
            oldVATRate = VATRate;
            oldOvertimeHourlyRate = OvertimeHourlyRate;
            oldMaxNumOfOvertimeHours = MaxNumOfOvertimeHours;
        }
        private void SaveConfig()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;
            Config config = new Config()
            {
                MaxNumOfOvertimeHours = MaxNumOfOvertimeHours,
                VATRate = VATRate,
                OvertimeHourlyRate = OvertimeHourlyRate,
            };
            configRepo.SaveConfig(config);
            UpdateOldValue();
        }
        private void ResetConfig()
        {
            VATRate = oldVATRate;
            MaxNumOfOvertimeHours = oldMaxNumOfOvertimeHours;
            OvertimeHourlyRate= oldOvertimeHourlyRate;
        }
    }

}
