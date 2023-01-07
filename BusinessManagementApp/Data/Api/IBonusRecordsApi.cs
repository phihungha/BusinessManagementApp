using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IBonusRecordsApi
    {
        [Get("/{month}/{year}")]
        IObservable<List<BonusRecord>> GetBonusRecords(int month, int year);

        [Post("/{month}/{year}")]
        IObservable<List<BonusRecord>> UpdateBonusRecords(
            int month,
            int year,
            [Body] IEnumerable<BonusRecord> records);
    }
}