using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CompletionTime { get; set; }

        public Employee EmployeeInCharge { get; set; }

        public Customer Customer { get; set; }

        public string Address { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItem> Items { get; set; }

        public decimal TotalPrice { get; set; }

        public List<Voucher> AppliedVouchers { get; set; }
             
        // Price after discount, before tax
        public decimal NetPrice { get; set; }

        public double VATRate { get; set; }

        // Net price after VAT tax
        public double TotalAmount { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Canceled,
        Completed,
        Returned
    }

    public class OrderItem
    {
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}