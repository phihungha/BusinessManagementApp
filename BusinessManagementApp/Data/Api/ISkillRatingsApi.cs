using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface ISkillRatingsApi
    {
        [Get("/")]
        IObservable<List<SkillOverview>> GetSkillOverviews();

        [Get("/{employeeId}")]
        IObservable<List<Skill>> GetEmployeeSkills(string employeeId);

        [Post("/")]
        IObservable<List<Skill>> UpdateEmployeeSkills(string employeeId, [Body] IEnumerable<Skill> skillRatings);
    }
}