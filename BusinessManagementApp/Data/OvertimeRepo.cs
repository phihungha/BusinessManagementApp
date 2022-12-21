using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class OvertimeRepo
    {
        public IObservable<List<OvertimeOverview>> GetOvertimeOverviews(int year, int month)
        {
            var overviews = new List<OvertimeOverview>();
            if (year == 2022 && month == 12)
            {
                overviews = new List<OvertimeOverview>()
                {
                    new OvertimeOverview()
                    {
                        MonthYear = new DateTime(2022, 12, 1),
                        Employee = new Employee() { Id = "1", Name = "Nguyen Van A"},
                        NumOfOvertimeDays = 2,
                        AvgOvertimeDuration = 1.5,
                        TotalOvertimePay = 500_000
                    },
                    new OvertimeOverview()
                    {
                        MonthYear = new DateTime(2022, 12, 1),
                        Employee = new Employee() { Id = "2", Name = "Mai Thi Xuan"},
                        NumOfOvertimeDays = 0,
                        AvgOvertimeDuration = 0,
                        TotalOvertimePay = 0
                    }
                };
            }

            return Observable.FromAsync(() => Task.FromResult(overviews));
        }

        public IObservable<OvertimeOverview> GetOvertimeDetails(string employeeId, int year, int month)
        {
            var overview = new OvertimeOverview()
            {
                MonthYear = new DateTime(2022, 12, 1),
                Employee = new Employee() { Id = "1", Name = "Nguyen Van A" },
                NumOfOvertimeDays = 2,
                AvgOvertimeDuration = 1.5,
                TotalOvertimePay = 500_000,
                Records = new List<OvertimeRecord>()
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
                }
            };

            return Observable.FromAsync(() => Task.FromResult(overview));
        }

        public IObservable<List<OvertimeRecord>> UpdateOvertimeRecords(string employeeId, List<OvertimeRecord> records)
        {
            return Observable.FromAsync(() => Task.FromResult(records));
        }
    }
}