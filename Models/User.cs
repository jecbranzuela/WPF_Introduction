using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Description { get; set; }

        public User(string? name, string? email, DateTime? birthday, string? description)
        {
            Name = name;
            Email = email;
            Birthday = birthday;
            Description = description;
        }
        public User()
        {
            
        }
    }
}
