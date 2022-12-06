namespace BusinessManagementApp.Data.Model
{
    public class VoucherType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public double Discount { get; set; }

        public decimal RequireProductCount { get; set; }

        public decimal RequireMinValue { get; set; }
    }

    public enum DiscountType
    {
        Percent,
        Raw
    }
}