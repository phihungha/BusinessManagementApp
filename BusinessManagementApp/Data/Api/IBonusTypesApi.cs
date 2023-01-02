using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface IBonusTypesApi
    {
        [Get("/")]
        IObservable<List<BonusType>> GetBonusTypes();

        [Get("/{id}")]
        IObservable<BonusType> GetBonusType(int id);

        [Post("/")]
        IObservable<BonusType> SaveBonusType([Body] BonusType skillType);

        [Patch("/{id}")]
        IObservable<BonusType> UpdateBonusType(int id, [Body] BonusType skillType);

        [Delete("/{id}")]
        IObservable<object> DeleteBonusType(int id);
    }
}