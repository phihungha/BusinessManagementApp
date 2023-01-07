using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class BonusTypesRepo
    {
        private IBonusTypesApi api;

        public BonusTypesRepo(IBonusTypesApi api)
        {
            this.api = api;
        }

        public IObservable<List<BonusType>> GetBonusTypes()
        {
            return api.GetBonusTypes();
        }

        public IObservable<BonusType> GetBonusType(int id)
        {
            return api.GetBonusType(id);
        }

        public IObservable<BonusType> AddBonusType(BonusType type)
        {
            return api.SaveBonusType(type);
        }

        public IObservable<BonusType> UpdateBonusType(int id, BonusType type)
        {
            return api.UpdateBonusType(id, type);
        }

        public IObservable<object> DeleteBonusType(int id)
        {
            return api.DeleteBonusType(id);
        }
    }
}