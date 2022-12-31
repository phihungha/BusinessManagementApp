using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface ISkillsApi
    {
        [Get("/")]
        IObservable<List<SkillOverview>> GetSkillOverviews();

        [Get("/{employeeId}")]
        IObservable<List<Skill>> GetEmployeeSkills(string employeeId);

        [Post("/")]
        IObservable<List<Skill>> UpdateEmployeeSkills(string employeeId, [Body] IEnumerable<Skill> skillRatings);
    }

    // TODO
    public interface ISkillTypesApi
    {
        [Get("/")]
        IObservable<List<SkillType>> GetSkillTypes();

        [Get("/{id}")]
        IObservable<SkillType> GetSkillType(int id);

        [Post("/")]
        IObservable<SkillType> SaveSkillType([Body] SkillType skillType);

        [Patch("/{id}")]
        IObservable<SkillType> UpdateSkillType(int id, [Body] SkillType skillType);

        [Delete("/{id}")]
        IObservable<object> DeleteSkillType(int id);
    }
}