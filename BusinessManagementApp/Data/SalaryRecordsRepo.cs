using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class SalaryRecordsRepo
    {
        private ISalaryRecordsApi api;

        public SalaryRecordsRepo(ISalaryRecordsApi api)
        {
            this.api = api;
        }

        public IObservable<List<SalaryRecord>> GetSalaryRecords(int year, int month)
        {
            return api.GetSalaryRecords(year, month);
        }
    }
}