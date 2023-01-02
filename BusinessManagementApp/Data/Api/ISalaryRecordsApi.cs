using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface ISalaryRecordsApi
    {
        [Get("/")]
        IObservable<List<SalaryRecord>> GetSalaryRecords();
    }
}