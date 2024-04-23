using System;
using System.Collections;
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
        private List<User> _allUsers;
        public ObservableCollection<User> UsersToShow { get; set; } = new ObservableCollection<User>();
        #region Commands
        public ICommand ShowAddUserWindowCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public ICommand EditEntryCommand { get; set; }
        public ICommand FirstCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand LastCommand { get; set; }

        #endregion Commands

        #region Fields
        private List<int> _recordOptions = new List<int>() { -1, 10, 15, 20, 25 }; //combobox options
        private User _selectedUser;
        private int _selectedIndex;
        private int _selectedValuePage;
        private int _currentPage;
        private int _numberOfPages;

        private bool _isFirstEnabled = true;
        private bool _isPreviousEnabled = true;
        private bool _isNextEnabled = true;
        private bool _isLastEnabled = true;
        #endregion Fields

        #region Properties
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
                UpdatePageBtnStates();
            }
        }
        public int SelectedValuePage
        {
            get { return _selectedValuePage; }
            set
            {
                _selectedValuePage = value;
                OnPropertyChanged(nameof(SelectedValuePage));
                UpdatePages();
                CurrentPage = 1;

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

        public bool IsFirstEnabled
        {
            get { return _isFirstEnabled; }
            set
            {
                _isFirstEnabled = value;
                OnPropertyChanged(nameof(IsFirstEnabled));
            }
        }
        public bool IsPreviousEnabled
        {
            get { return _isPreviousEnabled; }
            set
            {
                _isPreviousEnabled = value;
                OnPropertyChanged(nameof(IsPreviousEnabled));
            }
        }
        public bool IsNextEnabled
        {
            get { return _isNextEnabled; }
            set
            {
                _isNextEnabled = value;
                OnPropertyChanged(nameof(IsNextEnabled));
            }
        }
        public bool IsLastEnabled
        {
            get { return _isLastEnabled; }
            set
            {
                _isLastEnabled = value;
                OnPropertyChanged(nameof(IsLastEnabled));
            }
        }
        #endregion Properties

        #region Constructor
        public MainViewModel()
        {
            _allUsers = UserManagement.GetUsers().ToList();
            CurrentPage = 1;
            SelectedValuePage = -1;
            UpdatePages();
            ShowAddUserWindowCommand = new RelayCommand(ShowAddWindow, CanShowAddWindow);
            DeleteEntryCommand = new RelayCommand(DeleteEntry, CanDeleteEntry);
            EditEntryCommand = new RelayCommand(EditEntry, (s) => true);

            //pagination
            FirstCommand = new RelayCommand(FirstPage, (s) => true);
            PreviousCommand = new RelayCommand(PreviousPage, (s) => true);
            NextCommand = new RelayCommand(NextPage, (s) => true);
            LastCommand = new RelayCommand(LastPage, (s) => true);
        }
        #endregion Constructor

        #region CRUD Functions
        private bool CanShowAddWindow(object obj)
        {
            return true;
        }

        private void ShowAddWindow(object obj)
        {
            //create instance of AddUserWindow view
            AddUserWIndow addUserWIndow = new AddUserWIndow();
            AddUserViewModel addUserVM = new AddUserViewModel();
            addUserVM.MainVMContext = this;
            addUserWIndow.DataContext = addUserVM;

            addUserWIndow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addUserWIndow.Show(); //Actually show the window
        }
        public void DisplayNewUser(User newUser)
        {
            _allUsers.Add(newUser);
            UpdatePages();
            //CurrentPage++;

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
        #endregion CRUD Functions

        #region Pagination Logic
        private void UpdatePageBtnStates()
        {
            IsFirstEnabled = CurrentPage > 1;
            IsPreviousEnabled = CurrentPage > 1;
            IsNextEnabled = CurrentPage < NumberofPages;
            IsLastEnabled = CurrentPage < NumberofPages;
        }
        private void UpdatePages()
        {
            List<User> enumerable;
            if (SelectedValuePage == -1)
            {
                NumberofPages = 1;
                enumerable = _allUsers.ToList();
            }
            else
            {
                int result = _allUsers.Count / SelectedValuePage;
                int remainder = _allUsers.Count % SelectedValuePage;
                NumberofPages = result + (remainder > 0 ? 1:0); //Ternary Operator

                int startFrom;
                if (CurrentPage != 1) startFrom = (CurrentPage-1) * SelectedValuePage;
                else startFrom = 0;

                enumerable = _allUsers.Skip(startFrom).Take(SelectedValuePage).ToList();
            }
            UpdatePageBtnStates();
            UpdateUsersToShowList(enumerable);
        }
        private void UpdateUsersToShowList(IEnumerable<User> enumerable)
        {
            var temp = enumerable.ToList();
            UsersToShow.Clear();
            foreach(var user in temp)
            {
                UsersToShow.Add(user);
            }
        }
        private void LastPage(object obj)
        {
            var recordsToSkip = SelectedValuePage*(NumberofPages - 1);
            //var usersToShow = _allUsers.Skip(_allUsers.Count - SelectedValuePage).ToList();
            var usersToShow = _allUsers.Skip(recordsToSkip).ToList();
            UpdateUsersToShowList(usersToShow);
            CurrentPage = NumberofPages;
        }
        private void NextPage(object obj)
        {
            var startFrom = CurrentPage * SelectedValuePage;
            var usersToShow = _allUsers.Skip(startFrom).Take(SelectedValuePage).ToList();
            UpdateUsersToShowList(usersToShow);
            CurrentPage++;
        }
        private void PreviousPage(object obj)
        {
            CurrentPage--;
            var startFrom = _allUsers.Count() - (SelectedValuePage*(NumberofPages-(CurrentPage-1)));
            var usersToShow = _allUsers.Skip(startFrom).Take(SelectedValuePage);
            UpdateUsersToShowList(usersToShow);
        }
        private void FirstPage(object obj)
        {
            var usersToShow = _allUsers.Take(SelectedValuePage);
            UpdateUsersToShowList(usersToShow);
            CurrentPage = 1;
        }
        #endregion Pagination Logic
    }
}
