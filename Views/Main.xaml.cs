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
    }
}
