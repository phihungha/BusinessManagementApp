using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class BonusTypesRepo
    {
        public IObservable<List<BonusType>> GetBonusTypes()
        {
            var types = new List<BonusType>()
            {
                new BonusType()
                {
                    Id = 1,
                    Name = "Excellent",
                    Description = "For employees with A+ assessment score",
                    Amount = 1_000_000
                },
                new BonusType()
                {
                    Id = 2,
                    Name = "Good",
                    Description = "For employees with A assessment score",
                    Amount = 500_000
                }
            };

            return Observable.FromAsync(() => Task.FromResult(types));
        }

        public IObservable<BonusType> GetBonusType(int id)
        {
            var type = new BonusType()
            {
                Id = 1,
                Name = "Excellent",
                Description = "For employees with A+ assessment score",
                Amount = 1_000_000
            };

            return Observable.FromAsync(() => Task.FromResult(type));
        }

        public IObservable<BonusType> AddBonusType(BonusType type)
        {
            return Observable.FromAsync(() => Task.FromResult(type));
        }

        public IObservable<BonusType> UpdateBonusType(int id, BonusType type)
        {
            return Observable.FromAsync(() => Task.FromResult(type));
        }

        public IObservable<object> DeleteBonusType(int id)
        {
            return Observable.FromAsync(() => Task.FromResult(new object()));
        }
    }
}