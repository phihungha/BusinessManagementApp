using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessManagementApp.Data.Model
{
    public class Voucher : IEquatable<Voucher>
    {
        [JsonProperty("code")] public string Code { get; set; }

        // Only returns Id, Name on list. Return everything on details.
        [JsonProperty("type")] public VoucherType Type { get; set; }

        [JsonProperty("release_date")] public DateTime ReleaseDate { get; set; }

        [JsonProperty("expire_date")] public DateTime ExpiryDate { get; set; }

        public bool Equals(Voucher? other)
        {
            if (other == null) return false;

            return Code == other.Code;
        }
    }

    public class VoucherType
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("discount_type")] public DiscountType DiscountType { get; set; }

        [JsonProperty("discount")] public decimal DiscountValue { get; set; }

        // Min total price
        [JsonProperty("min_net_price")] public decimal MinNetPrice { get; set; }

        // Only return Id and Name
        [JsonProperty("applied_products")] public List<Product> AppliedProducts { get; set; }
    }

    public enum DiscountType
    {
        [Description("Percentage")] Percentage,

        [Description("Raw value")] Amount
    }
}