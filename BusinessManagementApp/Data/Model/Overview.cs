using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Overview
    {
        public int NumOfPendingOrders { get; set; }

        public int TotalStock { get; set; }

        public int TodayRevenue { get; set; }

        public List<Order> PendingOrders { get; set; }
    }
}