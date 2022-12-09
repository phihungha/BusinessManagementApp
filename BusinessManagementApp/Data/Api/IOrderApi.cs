using BusinessManagementApp.Data.Model;
using Refit;
using System.Collections.Generic;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IOrderApi
    {
        [Get("/")]
        IObservable<List<Order>> GetOrders();

        [Get("/{id}")]
        IObservable<Order> GetOrder(int id);

        [Post("/")]
        IObservable<Order> SaveOrder(Order Order);

        [Patch("/{id}")]
        IObservable<Order> UpdateOrder(int OrderId, Order request);

        [Delete("/{id}")]
        IObservable<object> DeleteOrder(int id);
        //BillDetail

    }
}
