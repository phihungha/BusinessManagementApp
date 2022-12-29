using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
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
