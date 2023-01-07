using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class OrdersRepo
    {
        private IOrdersApi api;

        public OrdersRepo(IOrdersApi api)
        {
            this.api = api;
        }

        public IObservable<Order> GetOrder(int id)
        {
            return api.GetOrder(id);
        }

        public IObservable<List<Order>> GetOrders()
        {
            return api.GetOrders();
        }

        public IObservable<Order> AddOrder(Order order)
        {
            return api.SaveOrder(order);
        }

        public IObservable<Order> UpdateOrder(int orderId, Order order)
        {
            return api.UpdateOrder(orderId, order);
        }
    }
}