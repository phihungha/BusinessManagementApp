using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IOrdersApi
    {
        [Get("/")]
        IObservable<List<Order>> GetOrders();

        [Get("/{id}")]
        IObservable<Order> GetOrder(int id);

        [Post("/")]
        IObservable<Order> SaveOrder([Body] Order order);

        [Patch("/{id}")]
        IObservable<Order> UpdateOrder(int id, [Body] Order order);

        [Delete("/{id}")]
        IObservable<object> DeleteOrder(int id);
    }
}