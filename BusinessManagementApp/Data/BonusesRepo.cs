using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class BonusesRepo
    {
        public IObservable<List<BonusRecord>> GetBonusRecords(int year, int month)
        {
            var records = new List<BonusRecord>();
            if (year == 2022 && month == 12)
            {
                records = new List<BonusRecord>()
                {
                    new BonusRecord()
                    {
                        MonthYear = new DateTime(2022, 12, 1),
                        Employee = new Employee() { Id = "1", Name = "Nguyen Van A"},
                        Amount = 1_000_000,
                        Type = new BonusType() { Id = 1, Name = "Excellent" }
                    },
                    new BonusRecord()
                    {
                        MonthYear = new DateTime(2022, 12, 1),
                        Employee = new Employee() { Id = "2", Name = "Mai Thi Xuan"},
                        Amount = 500_000,
                        Type = new BonusType() { Id = 2, Name = "Good" }
                    }
                };
            }

            return Observable.FromAsync(() => Task.FromResult(records));
        }

        public IObservable<List<BonusRecord>> UpdateBonusRecords(int year, int month, List<BonusRecord> records)
        {
            records = new List<BonusRecord>()
            {
                new BonusRecord()
                {
                    MonthYear = new DateTime(2022, 12, 1),
                    Employee = new Employee() { Id = "1", Name = "Nguyen Van A"},
                    Amount = 1_000_000,
                    Type = new BonusType() { Id = 1, Name = "Excellent" }
                },
                new BonusRecord()
                {
                    MonthYear = new DateTime(2022, 12, 1),
                    Employee = new Employee() { Id = "2", Name = "Mai Thi Xuan"},
                    Amount = 1_000_000,
                    Type = new BonusType() { Id = 1, Name = "Excellent" }
                }
            };

            return Observable.FromAsync(() => Task.FromResult(records));
        }
    }
}