using System;

namespace BusinessManagementApp.Data.Model
{
    public class Voucher
    {
        public string Code { get; set; }

        public int TypeId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}