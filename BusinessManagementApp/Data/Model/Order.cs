using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public string Address { get; set; }

        public OrderStatus Status { get; set; }

        public string CustomerId { get; set; }

        public List<OrderItem> Details { get; set; }
    }

    public enum OrderStatus
    {
        Waiting,
        Transporting,
        Success
    }
}