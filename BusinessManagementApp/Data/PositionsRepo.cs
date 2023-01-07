using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class PositionsRepo
    {
        private IPositionsApi api;

        public PositionsRepo(IPositionsApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeletePosition(int id)
        {
            return api.DeletePosition(id);
        }

        public IObservable<Position> GetPosition(int id)
        {
            return api.GetPosition(id);
        }

        public IObservable<List<Position>> GetPositions()
        {
            return api.GetPositions();
        }

        public IObservable<Position> AddPosition(Position position)
        {
            return api.SavePosition(position);
        }

        public IObservable<Position> UpdatePosition(int positionId, Position position)
        {
            return api.UpdatePosition(positionId, position);
        }
    }
}