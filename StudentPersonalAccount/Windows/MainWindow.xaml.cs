using Microsoft.EntityFrameworkCore;
using StudentPersonalAccount.DBContext;
using StudentPersonalAccount.MVVM.View;
using StudentPersonalAccount.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentPersonalAccount.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool OpenState = false;

        public MainWindow()
        {
            InitializeComponent();
            OpenState = true;
            mainControl.Content = new HomeView();
            GetUser();
        }

        private void GetUser()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == AuthenticationView.Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    FSNameLabel.Content = $"{user.UserData?.SecondName} {user.UserData?.FirstName}";
                    GNLabel.Content = user.UserData?.GroupNumber;
                }
            }
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new ProfileView();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new HomeView();
        }

        private void profileImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainControl.Content = new ProfileView();
            profileButton.IsChecked = true;
        }
    }
}
