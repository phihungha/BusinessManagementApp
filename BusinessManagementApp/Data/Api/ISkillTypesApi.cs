using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
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