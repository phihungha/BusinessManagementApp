using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class SkillRecordsRepo
    {
        private ISkillRecordsApi api;

        public SkillRecordsRepo(ISkillRecordsApi api)
        {
            this.api = api;
        }

        public IObservable<List<SkillOverview>> GetSkillOverviews()
        {
            return api.GetSkillOverviews();
        }

        public IObservable<SkillOverview> GetSkillDetails(string employeeId)
        {
            return api.GetSkillDetails(employeeId);
        }

        public IObservable<List<SkillRecord>> UpdateSkills(string employeeId, List<SkillRecord> skills)
        {
            return api.UpdateEmployeeSkills(employeeId, skills);
        }
    }
}