using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IOvertimeRecordsApi
    {
        [Get("/{year}/{month}")]
        IObservable<List<OvertimeOverview>> GetOvertimeOverviews(int year, int month);

        [Get("/{employeeId}/{year}/{month}")]
        IObservable<OvertimeOverview> GetOvertimeDetails(string employeeId, int year, int month);

        [Post("/{employeeId}/{year}/{month}")]
        IObservable<List<OvertimeRecord>> UpdateEmployeeOvertimeRecords(
            string employeeId,
            int year,
            int month,
            [Body] IEnumerable<OvertimeRecord> overtimeRecords);
    }
}