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
        IObservable<SkillOverview> GetSkillDetails(string employeeId);

        [Post("/{employeeId}")]
        IObservable<List<SkillRecord>> UpdateEmployeeSkills(string employeeId, [Body] IEnumerable<SkillRecord> skillRecords);
    }
}