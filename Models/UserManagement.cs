using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    public class UserManagement
    {
        public static ObservableCollection<User> DatabaseUsers { get; set; } = new ObservableCollection<User>()
        {
            //add pre-generated data into our app
            new User("Matthew","Matthew@addu.edu.ph",new DateTime(1995,10,5),"Cute. Hardworking. A sa Diffcal"),
            new User("Judas","judas@hotmail.com",new DateTime(1978,05,5),"Medyo bastos. Backstabber"),
            new User("Jesus","jesus@godmail.com",new DateTime(2024,03,30),"Savior")
        };
        public static ObservableCollection<User> GetUsers()
        {
            return DatabaseUsers;
        }
        public static void AddUser(User user)
        {
            DatabaseUsers.Add(user);
        }
        public static void DeleteUser(User user)
        {
            DatabaseUsers.Remove(user);
        }
    }
}
