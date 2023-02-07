using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Order
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("creation_time")] public DateTime CreationTime { get; set; }

        [JsonProperty("completion_time")] public DateTime? CompletionTime { get; set; }

        // Only include Id and Name
        [JsonProperty("employee_in_charge")] public Employee EmployeeInCharge { get; set; }

        [JsonProperty("customer")] public Customer Customer { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("status")] public OrderStatus Status { get; set; }

        // Only return when getting details
        [JsonProperty("items")] public List<OrderItem> Items { get; set; }

        [JsonProperty("total_price")] public decimal TotalPrice { get; set; }

        // Only return when getting details
        [JsonProperty("applied_vouchers")] public List<Voucher> AppliedVouchers { get; set; }

        // Price after discount, before tax
        [JsonProperty("net_price")] public decimal NetPrice { get; set; }

        [JsonProperty("vat_rate")] public double VATRate { get; set; }

        // Net price after VAT tax
        [JsonProperty("total_amount")] public decimal TotalAmount { get; set; }
    }
}