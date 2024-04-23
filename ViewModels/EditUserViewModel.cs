using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using UserManagementSystem.Commands;
using UserManagementSystem.Models;

namespace UserManagementSystem.ViewModels
{
    class EditUserViewModel : NotifyPropertyChanged
    {
        public ICommand SaveChangesCommand { get; set; } //this command will be sent to the relay command
        public ICommand CancelCommand { get; set; } //this command will be sent to the relay command

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public string? _newName;
        public string? NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        public string? _newEmail;
        public string? NewEmail
        {
            get { return _newEmail; }
            set
            {
                _newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
            }
        }
        private DateTime? _newBirthDay;
        public DateTime? NewBirthDay
        {
            get { return _newBirthDay; }
            set
            {
                // Handle empty strings by converting them to null
                //_birthday=value;
                if (string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    _newBirthDay = null;
                }
                else
                {
                    _newBirthDay = value;
                }
                OnPropertyChanged(nameof(NewBirthDay));
            }
        }
        public string? _newDescription;
        public string? NewDescription
        {
            get { return _newDescription; }
            set
            {
                _newDescription = value;
                OnPropertyChanged(nameof(NewDescription));
            }
        }

        public EditUserViewModel(User selectedUser)
        {
            SelectedUser = selectedUser;
            NewName = SelectedUser.Name;
            NewEmail = SelectedUser.Email;
            NewBirthDay = SelectedUser.BirthDay;
            _newDescription = SelectedUser.Description;
            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new RelayCommand(CancelChanges,(s)=>true);
        }
        private void SaveChanges(object obj)
        {
            UserManagement.EditUser(SelectedUser, NewName, NewEmail, NewBirthDay, NewDescription);
            var editUserWin = obj as Window;
            editUserWin.Close();

            // Show a message box confirming the addition of the user
            string message = $"Changes saved!";
            string caption = "Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show(message, caption, button, icon);
        }

        private bool CanSaveChanges(object obj)
        {
            return true;
        }
        private void CancelChanges(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
