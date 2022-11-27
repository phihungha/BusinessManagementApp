using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class Product
    {

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Unit { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int[] Categories { get; set; }

    }
}
