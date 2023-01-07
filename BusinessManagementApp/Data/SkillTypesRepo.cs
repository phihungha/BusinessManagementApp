using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class SkillTypesRepo
    {
        private ISkillTypesApi api;

        public SkillTypesRepo(ISkillTypesApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeleteSkillType(int id)
        {
            return api.DeleteSkillType(id);
        }

        public IObservable<SkillType> GetSkillType(int id)
        {
            return api.GetSkillType(id);
        }

        public IObservable<List<SkillType>> GetSkillTypes()
        {
            return api.GetSkillTypes();
        }

        public IObservable<SkillType> AddSkillType(SkillType skillType)
        {
            return api.SaveSkillType(skillType);
        }

        public IObservable<SkillType> UpdateSkillType(int skillTypeId, SkillType skillType)
        {
            return api.UpdateSkillType(skillTypeId, skillType);
        }
    }
}