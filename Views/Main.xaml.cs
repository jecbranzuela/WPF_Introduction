using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserManagementSystem.ViewModels;
using UserManagementSystem.Models;

namespace UserManagementSystem.Views
{
    /// <summary>
    /// Code Behind
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            //explicitly linked the MainViewModel to the Main View
            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //derived from ListView w/ Name = UserList
            UserList.Items.Filter = FilterMethod;
        }
        private bool FilterMethod(object obj)
        {
            var user = (User)obj;
            return user.Name.Contains(FilterTextBox.Text,StringComparison.OrdinalIgnoreCase);
            //add argument to disregard case sensitivity
        }
    }
}
