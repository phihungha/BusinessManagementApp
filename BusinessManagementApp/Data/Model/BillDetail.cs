﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class BillDetail
    {

        public int BillId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

    }
}
