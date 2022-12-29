using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CompletionTime { get; set; }

        // Only include Id and Name
        public Employee EmployeeInCharge { get; set; }

        public Customer Customer { get; set; }

        public string Address { get; set; }

        public OrderStatus Status { get; set; }

        // Only return when getting details
        public List<OrderItem> Items { get; set; }

        public decimal TotalPrice { get; set; }

        // Only return when getting details
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
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}