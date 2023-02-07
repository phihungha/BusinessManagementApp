using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using System;
using System.ComponentModel;

namespace BusinessManagementApp.ViewModels.Filters
{
    public enum OrderInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Customer name")]
        CustomerName,

        [Description("Employee name")]
        EmployeeName,

        [Description("Address")]
        Address,
    }

    public class OrderContainsFilter : AbstractFilter<Order>
    {
        public OrderInfoSearchBy SearchBy { get; set; } = OrderInfoSearchBy.Id;

        public string SearchText { get; set; } = string.Empty;

        public OrderContainsFilter(AbstractFilter<Order>? filter = null)
        : base(filter) { }

        protected override bool PerformFiltering(Order order)
        {
            if (SearchText == null)
                return true;

            var containsCheckMode = StringComparison.InvariantCultureIgnoreCase;
            bool result;
            switch (SearchBy)
            {
                case OrderInfoSearchBy.CustomerName:
                    result = order.Customer.Name.Contains(SearchText, containsCheckMode);
                    break;

                case OrderInfoSearchBy.EmployeeName:
                    result = order.Customer.Name.Contains(SearchText, containsCheckMode);
                    break;

                case OrderInfoSearchBy.Id:
                    result = order.Id.ToString().Contains(SearchText, containsCheckMode);
                    break;

                case OrderInfoSearchBy.Address:
                    result = order.Address.Contains(SearchText, containsCheckMode);
                    break;

                default:
                    throw new ArgumentException(nameof(SearchBy));
            }

            return result;
        }
    }
}