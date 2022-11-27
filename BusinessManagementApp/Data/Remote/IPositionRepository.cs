using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Remote
{
    public interface IPositionRepository
    {
        IObservable<List<Position>> GetPositions();

        IObservable<Position> GetPosition(int id);

        IObservable<Position> SavePosition(Position Position);

        IObservable<Position> UpdatePosition(int PositionId, Position request);

        IObservable<Object> DeletePosition(int id);
    }
}
