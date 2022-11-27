using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class VoucherType
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public String Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public double Discount { get; set; }

        public decimal RequireProductCount { get; set; }

        public decimal RequireMinValue { get; set; }

    }
}
