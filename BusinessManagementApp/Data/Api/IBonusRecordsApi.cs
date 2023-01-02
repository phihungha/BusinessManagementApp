using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface IBonusRecordsApi
    {
        [Get("/{year}/{month}")]
        IObservable<List<BonusRecord>> GetBonusRecords(int month, int year);

        [Post("/{year}/{month}")]
        IObservable<List<BonusRecord>> UpdateBonusRecords(
            int month,
            int year,
            [Body] IEnumerable<BonusRecord> bonusRecords);
    }
}