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
    public class MainViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }
        private List<int> _recordOptions = new List<int>() { 10, 15, 20, 25 }; //combobox options
        public ICommand ShowAddUserWindowCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public ICommand EditEntryCommand { get; set; }
        public ICommand FirstCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand LastCommand { get; set; }
        private User _selectedUser;
        private int _selectedIndex;
        private int _selectedValuePage;
        private int _currentPage = 1;
        private int _numberOfPages;

        private bool _isFirstEnabled = true;
        private bool _isPreviousEnabled = true;
        private bool _isNextEnabled = true;
        private bool _isLastEnabled = true;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        public List<int> RecordOptions
        {
            get { return _recordOptions; }
            set
            {
                _recordOptions = value;
                OnPropertyChanged(nameof(RecordOptions));
            }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public int SelectedValuePage
        {
            get { return _selectedValuePage; }
            set
            {
                _selectedValuePage = value;
                OnPropertyChanged(nameof(SelectedValuePage));
            }
        }
        public int NumberofPages
        {
            get { return _numberOfPages; }
            set
            {
                _numberOfPages = value;
                OnPropertyChanged(nameof(NumberofPages));
            }
        }
        public MainViewModel()
        {
            Users = UserManagement.GetUsers();
            ShowAddUserWindowCommand = new RelayCommand(ShowAddWindow, CanShowAddWindow);
            DeleteEntryCommand = new RelayCommand(DeleteEntry,CanDeleteEntry);
            EditEntryCommand = new RelayCommand(EditEntry, (s)=>true);

            //pagination
            FirstCommand = new RelayCommand(FirstPage, (s) => true);
            PreviousCommand = new RelayCommand(PreviousPage, (s) => true);
            NextCommand = new RelayCommand(NextPage, (s) => true);
            LastCommand = new RelayCommand(LastPage, (s) => true);
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
        private void EditEntry(object obj)
        {
            var editUserVM = new EditUserViewModel(SelectedUser);

            EditUserWindow editUserWin = new EditUserWindow();
            editUserWin.DataContext = editUserVM;
            editUserWin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editUserWin.Show();
        }
        private void LastPage(object obj)
        {
            CurrentPage = NumberofPages;
        }
        private void NextPage(object obj)
        {
            if (CurrentPage < NumberofPages)
            {
                CurrentPage++;
            }
        }
        private void PreviousPage(object obj)
        {
            if (CurrentPage < 1)
            {
                CurrentPage--;
            }
        }
        private void FirstPage(object obj)
        {
            CurrentPage = 1;
        }
    }
}
