using Bogus;
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
        public static ObservableCollection<User> DatabaseUsers { get; set; } = new ObservableCollection<User>();
        public static ObservableCollection<User> GetUsers()
        {
            var faker = new Faker();
            faker.Random = new Randomizer(123); //use the same seed for consistent data
            int numberOfUsersToGenerate = 100;
            for (int i = 0;i < numberOfUsersToGenerate; i++)
            {
                string name = faker.Name.FullName();
                string email = faker.Internet.Email();
                var fromDate = new DateOnly(1985, 1,1);
                var endDate = new DateOnly(2005,1,1);
                var birthDay = faker.Date.BetweenDateOnly(fromDate, endDate).ToDateTime(TimeOnly.MinValue);
                string description = faker.Lorem.Paragraph();

                var userId = i+1;
                var user = new User(userId,name, email,birthDay,description);
                DatabaseUsers.Add(user);
            }
            return DatabaseUsers;
        }
        public static void AddUser(User user)
        {
            user.Id = DatabaseUsers.Count() + 1;
            DatabaseUsers.Add(user);
        }
        public static void DeleteUser(User user)
        {
            DatabaseUsers.Remove(user);
        }
        public static void EditUser(User selectedUser,string? name, string? email, DateTime? birthday, string? description)
        {
            selectedUser.Name = name;
            selectedUser.Email = email;
            selectedUser.BirthDay = birthday;
            selectedUser.Description = description;
        }
    }
}
