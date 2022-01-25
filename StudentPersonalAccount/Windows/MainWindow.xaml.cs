using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
        public static string? FileName;

        string Login;

        public MainWindow(string login)
        {
            InitializeComponent();
            Login = login;
            OpenState = true;
            mainControl.Content = new HomeView();
            GetUser();
            CheckProfileImageChange();
            CheckProfileImageChange("");
        }

        private void GetUser()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    FSNameLabel.Content = $"{user.UserData?.SecondName} {user.UserData?.FirstName}";
                    GNLabel.Content = user.UserData?.GroupNumber;
                }
            }
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new ProfileView(Login);
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new HomeView();
        }

        private void profileImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainControl.Content = new ProfileView(Login);
            profileButton.IsChecked = true;
        }

        private void CheckProfileImageChange()
        {
            string? FileName = string.Empty;

            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    if (user.UserData?.ProfilePhoto != FileName)
                    {
                        FileName = user.UserData?.ProfilePhoto;
                        profileImage.ImageSource = new BitmapImage(new Uri($"Res/{FileName}", UriKind.Relative));
                    }
                }
            }
        }

        private async void CheckProfileImageChange(string hyi)
        {
            while (true)
            {
                await Task.Delay(1000);

                if (FileName != null)
                {
                    profileImage.ImageSource = new BitmapImage(new Uri($"Res/{FileName}", UriKind.Relative));
                }
            }

        }
    }
}
