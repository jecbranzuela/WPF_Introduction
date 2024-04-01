using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices; //CallerMemberName
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementSystem.Models;
namespace UserManagementSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand ShowWindowCommand { get; set; }

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
