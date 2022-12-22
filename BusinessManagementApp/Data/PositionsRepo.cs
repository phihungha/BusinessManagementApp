using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class PositionsRepo
    {
        public IObservable<List<Position>> GetPositions()
        {
            var positions = new List<Position>()
            {
                new Position() { Id = 1, Name = "Sales"},
                new Position() { Id = 2, Name = "Sales manager"},
                new Position() { Id = 3, Name = "HR manager"},
                new Position() { Id = 4, Name = "Director"},
            };

            return Observable.FromAsync(() => Task.FromResult(positions));
        }
    }
}