using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class OvertimeRecordsRepo
    {
        public IObservable<List<OvertimeOverview>> GetOvertimeOverviews(int year, int month)
        {
            var overviews = new List<OvertimeOverview>()
            {
                new OvertimeOverview()
                {
                    Employee = new Employee() { Id = "1", Name = "Nguyen Van A"},
                    NumOfOvertimeDays = 2,
                    AvgOvertimeDuration = 1.5,
                    TotalOvertimePay = 500_000
                },
                new OvertimeOverview()
                {
                    Employee = new Employee() { Id = "2", Name = "Mai Thi Xuan"},
                    NumOfOvertimeDays = 0,
                    AvgOvertimeDuration = 0,
                    TotalOvertimePay = 0
                }
            };

            return Observable.FromAsync(() => Task.FromResult(overviews));
        }

        public IObservable<List<OvertimeRecord>> GetSkills(string employeeId)
        {
            var records = new List<OvertimeRecord>()
            {
                new OvertimeRecord()
                {
                    EmployeeId = "1",
                    Date = new DateTime(2022, 12, 2),
                    From = 17,
                    To = 18
                },

                new OvertimeRecord()
                {
                    EmployeeId = "1",
                    Date = new DateTime(2022, 12, 18),
                    From = 17,
                    To = 19
                }
            };

            return Observable.FromAsync(() => Task.FromResult(records));
        }

        public IObservable<List<OvertimeRecord>> UpdateOvertimeRecords(string employeeId, List<OvertimeRecord> records)
        {
            return Observable.FromAsync(() => Task.FromResult(records));
        }
    }
}