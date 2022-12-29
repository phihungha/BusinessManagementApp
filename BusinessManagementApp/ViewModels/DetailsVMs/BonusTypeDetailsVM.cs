﻿using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class BonusTypeDetailsVM : ViewModelBase
    {
        #region Dependencies

        private BonusTypesRepo bonusTypesRepo;

        #endregion Dependencies

        #region Input properties

        private int id = 0;

        public int Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }

        private string name = string.Empty;

        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private decimal amount = 1000;

        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value, true);
        }

        private string description = string.Empty;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEditMode = false;

        public bool IsEditMode
        {
            get => isEditMode;
            private set
            {
                SetProperty(ref isEditMode, value);
            }
        }

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public BonusTypeDetailsVM(BonusTypesRepo bonusTypesRepo)
        {
            this.bonusTypesRepo = bonusTypesRepo;

            Save = new AsyncRelayCommand(SaveBonusType);
            Delete = new AsyncRelayCommand(DeleteBonusType);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.BonusTypes)
                );
        }

        public override async void LoadData(object? id = null)
        {
            if (id != null)
            {
                IsEditMode = true;
                await LoadBonusType((int)id);
            }

            CanSave = true;
        }

        private async Task LoadBonusType(int id)
        {
            BonusType bonusType = await bonusTypesRepo.GetBonusType(id);
            Id = bonusType.Id;
            Name = bonusType.Name;
            Description = bonusType.Description;
            Amount = bonusType.Amount;
        }

        private async Task SaveBonusType()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var bonusType = new BonusType()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Amount = Amount
            };

            if (IsEditMode)
            {
                await bonusTypesRepo.UpdateBonusType(Id, bonusType);
            }
            else
            {
                await bonusTypesRepo.AddBonusType(bonusType);
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.BonusTypes);
        }

        private async Task DeleteBonusType()
        {
            await bonusTypesRepo.DeleteBonusType(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.BonusTypes);
        }
    }
}