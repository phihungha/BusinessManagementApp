using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;

namespace BusinessManagementApp.ViewModels.Filters
{
    public class OrderStatusFilter : AbstractFilter<Order>
    {
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public OrderStatusFilter(AbstractFilter<Order>? filter = null)
        : base(filter) { }

        protected override bool PerformFiltering(Order input)
        {
            return input.Status == Status;
        }
    }
}