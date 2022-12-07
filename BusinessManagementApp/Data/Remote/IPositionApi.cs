using BusinessManagementApp.Data.Model;
using System.Collections.Generic;
using System;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IPositionApi
    {
        [Get("/")]
        IObservable<List<Position>> GetPositions();

        [Get("/{id}")]
        IObservable<Position> GetPosition(int id);

        [Post("/")]
        IObservable<Position> SavePosition(Position Position);

        [Patch("/{id}")]
        IObservable<Position> UpdatePosition(int PositionId, Position request);

        [Delete("/{id}")]
        IObservable<object> DeletePosition(int id);
    }
}
