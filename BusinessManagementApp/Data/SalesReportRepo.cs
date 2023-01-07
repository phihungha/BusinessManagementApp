using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;

namespace BusinessManagementApp.Data
{
    public class SalesReportRepo
    {
        private ISalesReportsApi api;

        public SalesReportRepo(ISalesReportsApi api)
        {
            this.api = api;
        }

        public IObservable<SalesStats> GetSalesReport(int year, int month)
        {
            return api.GetSalesStats(year, month);
        }
    }
}