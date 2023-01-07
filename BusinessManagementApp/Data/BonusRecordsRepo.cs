using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class BonusRecordsRepo
    {
        private IBonusRecordsApi api;

        public BonusRecordsRepo(IBonusRecordsApi api)
        {
            this.api = api;
        }

        public IObservable<List<BonusRecord>> GetBonusRecords(int year, int month)
        {
            return api.GetBonusRecords(month, year);
        }

        public IObservable<List<BonusRecord>> UpdateBonusRecords(int year, int month, List<BonusRecord> records)
        {
            return api.UpdateBonusRecords(month, year, records);
        }
    }
}