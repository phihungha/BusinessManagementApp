using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessManagementApp.Data.Model
{
    public class Voucher
    {
        public string Code { get; set; }

        // Only returns Id, Name
        public VoucherType Type { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }

    public class VoucherType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public double DiscountValue { get; set; }

        public decimal MinNetPrice { get; set; }

        // Only return Id and Name
        public List<Product> AppliedProducts { get; set; }
    }

    public enum DiscountType
    {
        [Description("Percentage")]
        Percent,

        [Description("Raw value")]
        Raw
    }
}