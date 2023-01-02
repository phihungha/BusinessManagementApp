using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IPositionsApi
    {
        [Get("/")]
        IObservable<List<Position>> GetPositions();

        [Get("/{id}")]
        IObservable<Position> GetPosition(int id);

        [Post("/")]
        IObservable<Position> SavePosition([Body] Position position);

        [Patch("/{id}")]
        IObservable<Position> UpdatePosition(int id, [Body] Position position);

        [Delete("/{id}")]
        IObservable<object> DeletePosition(int id);
    }
}
