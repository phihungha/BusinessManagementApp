using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class PositionsRepo
    {
        public IObservable<object> DeletePosition(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Position> GetPosition(int id)
        {
            var position = new Position()
            {
                Id = 1,
                Name = "director",
                Description = "a",
                SupplementSalary = 100000,
                CanManageAll = true,
            };

            return Observable.FromAsync(() => Task.FromResult(position));
        }

        public IObservable<List<Position>> GetPositions()
        {
            var positions = new List<Position>()
            {
                new Position()
                {
                    Id = 1,
                    Name = "Director",
                    Description = "a",
                    SupplementSalary = 100000,
                },

                new Position()
                {
                    Id = 2,
                    Name = "Sales",
                    Description = "b",
                    SupplementSalary = 500000,
                },
                new Position()
                {
                    Id = 3,
                    Name = "Sales manager",
                    Description = "b",
                    SupplementSalary = 500000,
                }
            };
            return Observable.FromAsync(() => Task.FromResult(positions));
        }

        public IObservable<Position> AddPosition(Position position)
        {
            throw new NotImplementedException();
        }

        public IObservable<Position> UpdatePosition(int positionId, Position position)
        {
            throw new NotImplementedException();
        }
    }
}