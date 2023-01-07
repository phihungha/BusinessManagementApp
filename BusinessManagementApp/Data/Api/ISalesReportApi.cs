using BusinessManagementApp.Data.Model;
using Refit;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface ISalesReportsApi
    {
        [Get("/{month}/{year}")]
        IObservable<SalesStats> GetSalesStats(int year, int month);
    }
}