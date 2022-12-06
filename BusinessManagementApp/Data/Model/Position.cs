using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}