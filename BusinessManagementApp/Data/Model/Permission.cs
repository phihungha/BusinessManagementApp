namespace BusinessManagementApp.Data.Model
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte ValueType { get; set; }

        public string StringValue { get; set; }

        public decimal IntValue { get; set; }

        public double DoubleValue { get; set; }

        public bool BooleanValue { get; set; }
    }
}