using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices; //CallerMemberName
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserManagementSystem.Commands;
using UserManagementSystem.Models;
using UserManagementSystem.Views;
namespace UserManagementSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand ShowAddUserWindowCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }

        private User _selectedUser;
        private int _selectedIndex;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }
        public MainViewModel()
        {
            Users = UserManagement.GetUsers();
            ShowAddUserWindowCommand = new RelayCommand(ShowAddWindow, CanShowAddWindow);
            DeleteEntryCommand = new RelayCommand(DeleteEntry,CanDeleteEntry);
        }

        private bool CanShowAddWindow(object obj)
        {
            return true;
        }

        private void ShowAddWindow(object obj)
        {
            //create instance of AddUserWindow view
            AddUserWIndow addUserWIndow = new AddUserWIndow();
            addUserWIndow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addUserWIndow.Show(); //Actually show the window
        }
        private bool CanDeleteEntry(object obj)
        {
            return true;
        }
        private void DeleteEntry(object obj)
        {
            UserManagement.DeleteUser(SelectedUser);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
