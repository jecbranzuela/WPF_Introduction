using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserManagementSystem.Commands;
using UserManagementSystem.Models;

namespace UserManagementSystem.ViewModels
{
    public class AddUserViewModel
    {
        //this command represents the action that should be done when the "save" button is clicked
        public ICommand AddUserCommand { get; set; } 
        public ICommand CancelCommand { get; set; } 
        private string? _name;
        private string? _email;
        private DateTime? _birthDay;
        private string? _description;
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string? Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public DateTime? BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public AddUserViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            CancelCommand = new RelayCommand(CancelOperation, CanCancelOperation);
        }

        private bool CanCancelOperation(object obj)
        {
            return true;
        }

        private void CancelOperation(object obj)
        {
            var addUserWindow = obj as Window;
            addUserWindow.Close();
        }

        private bool CanAddUser(object obj)
        {
            return true;
        }

        private void AddUser(object obj)
        {
            User newUser = new User(this.Name,this.Email,this.BirthDay,this.Description);
            UserManagement.AddUser(newUser);

            //close the current window
            var addUserWindow = obj as Window;
            addUserWindow.Close();

            //displays a MessageBox to prompt program users that the data has been successfully addded
            string message = $"{Name} has been added to the database";
            string caption = "Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(message, caption, button, icon);

        }
    }
}
