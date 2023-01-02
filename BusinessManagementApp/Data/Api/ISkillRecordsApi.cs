using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface ISkillRecordsApi
    {
        [Get("/")]
        IObservable<List<SkillOverview>> GetSkillOverviews();

        [Get("/{employeeId}")]
        IObservable<List<SkillRecord>> GetEmployeeSkills(string employeeId);

        [Post("/")]
        IObservable<List<SkillRecord>> UpdateEmployeeSkills(string employeeId, [Body] IEnumerable<SkillRecord> skillRatings);
    }
}