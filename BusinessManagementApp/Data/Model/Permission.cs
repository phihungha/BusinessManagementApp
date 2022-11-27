using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data.Model
{
    public class Permission
    {

        public int Id { get; set; }

        public String Name { get; set; }

        public byte ValueType { get; set; }

        public String StringValue { get; set; }

        public decimal IntValue { get; set; }

        public double DoubleValue { get; set; }

        public bool BooleanValue { get; set; }

    }
}
