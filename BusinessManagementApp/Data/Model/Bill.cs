using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class Bill
    {

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public String UserId { get; set; }

        public String Address { get; set; }

        public BillStatus Status {
            get;
            set;
        }

        public String CustomerId { get; set; }

        public List<BillDetail> Details { get; set; }

    }
}
