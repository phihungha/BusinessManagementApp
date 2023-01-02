using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface IOvertimeApi
    {
        [Get("/")]
        IObservable<List<OvertimeOverview>> GetOvertimeOverviews(string employeeId, int year, int month);

        [Get("/{employeeId}/{year}/{month}")]
        IObservable<List<OvertimeRecord>> GetEmployeeOvertimeRecords(string employeeId, int year, int month);

        [Post("/{employeeId}/{year}/{month}")]
        IObservable<List<OvertimeRecord>> UpdateEmployeeOvertimeRecords(
            string employeeId,
            int year,
            int month,
            [Body] IEnumerable<OvertimeRecord> overtimeRecords);
    }
}