using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class OvertimeRecordsRepo
    {
        private IOvertimeRecordsApi api;

        public OvertimeRecordsRepo(IOvertimeRecordsApi api)
        {
            this.api = api;
        }

        public IObservable<List<OvertimeOverview>> GetOvertimeOverviews(int year, int month)
        {
            return api.GetOvertimeOverviews(month, year);
        }

        public IObservable<OvertimeOverview> GetOvertimeDetails(string employeeId, int year, int month)
        {
            return api.GetOvertimeDetails(employeeId, year, month);
        }

        public IObservable<List<OvertimeRecord>> UpdateOvertimeRecords(string employeeId, int year, int month, List<OvertimeRecord> records)
        {
            return api.UpdateEmployeeOvertimeRecords(employeeId, year, month, records);
        }
    }
}