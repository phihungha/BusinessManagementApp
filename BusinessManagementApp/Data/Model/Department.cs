using System;

namespace BusinessManagementApp.Data.Model
{
    public class Department : IEquatable<Department>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        // Only include Id and Name
        public Employee Head { get; set; }

        // Supports WPF ComboBox binding.
        // Comparing Id is usually enough.
        public bool Equals(Department? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}