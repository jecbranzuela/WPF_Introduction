using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Commands;

namespace UserManagementSystem.Models
{
    public class User : NotifyPropertyChanged
    {
        private int? _id;
        public int? Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string? _email;
        public string? Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private DateTime? _birthDay;
        public DateTime? BirthDay
        {
            get { return _birthDay; }
            set
            {
                _birthDay = value;
                OnPropertyChanged(nameof(BirthDay));
            }
        }
        private string? _description;
        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public User(int id,string? name, string? email, DateTime? birthday, string? description)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDay = birthday;
            Description = description;
        }
        public User(string? name, string? email, DateTime? birthday, string? description)
        {
            Name = name;
            Email = email;
            BirthDay = birthday;
            Description = description;
        }
    }
}
