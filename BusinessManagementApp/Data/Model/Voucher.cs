using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class Voucher
    {

        public String code { get; set; }

        public int TypeId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime ExpiredDate { get; set; }

    }
}
