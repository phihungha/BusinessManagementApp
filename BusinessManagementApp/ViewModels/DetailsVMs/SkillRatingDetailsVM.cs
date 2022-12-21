﻿using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class SkillRatingDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private SkillsRepo skillsRepo;

        #endregion Dependencies

        public ObservableCollection<Skill> Skills { get; } = new();

        private Employee currentEmployee = new();

        public Employee CurrentEmployee
        {
            get => currentEmployee;
            set => SetProperty(ref currentEmployee, value);
        }

        #region Button enable/disable logic

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public SkillRatingDetailsVM(SkillsRepo skillsRepo)
        {
            this.skillsRepo = skillsRepo;

            Save = new AsyncRelayCommand(SaveSkillRatings);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillRating)
                );
        }

        public override async void LoadData(object? id = null)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            SkillOverview overview = await skillsRepo.GetSkillDetails((string)id);
            Skills.AddRange(overview.Skills);
            CurrentEmployee = overview.Employee;

            CanSave = true;
        }

        private async Task SaveSkillRatings()
        {
            await skillsRepo.UpdateSkills(CurrentEmployee.Id, Skills.ToList());
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillRating);
        }
    }
}