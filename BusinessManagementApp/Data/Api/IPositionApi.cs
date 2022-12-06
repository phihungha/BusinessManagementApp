using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IPositionApi
    {
        IObservable<List<Position>> GetPositions();

        IObservable<Position> GetPosition(int id);

        IObservable<Position> SavePosition(Position Position);

        IObservable<Position> UpdatePosition(int PositionId, Position request);

        IObservable<object> DeletePosition(int id);
    }
}
