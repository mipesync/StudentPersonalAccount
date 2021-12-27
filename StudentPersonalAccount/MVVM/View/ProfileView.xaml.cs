using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StudentPersonalAccount.DBContext;
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

namespace StudentPersonalAccount.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            CheckProfileImageChange();
        }

        private string? FileName;

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var usersAdd = context.Users.Where(p => p.Login == AuthenticationView.Login);

                User userAdd = new User();

                foreach (var _user in usersAdd)
                {
                    userAdd = _user;
                }

                string secondName = secondNameTextBox.Text,
                    firstName = firstNameTextBox.Text,
                    lastName = lastNameTextBox.Text,
                    groupNumber = groupNumberTextBox.Text,
                    phone = phoneTextBox.Text;

                var userData = new UserData { FirstName = firstName, SecondName = secondName,
                    LastName = lastName, GroupNumber = groupNumber, Phone = phone, User = userAdd};

                context.UserDatas.Add(userData);
                context.SaveChanges();
            }
        }
        private void profileImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            FileName = openFileDialog.FileName;       
            
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == AuthenticationView.Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    user.UserData.ProfilePhoto = FileName;
                }

                context.SaveChanges();
            }
            profileImage.ImageSource = new BitmapImage(new Uri(FileName));

            MainWindow mainWindow = new MainWindow();
            mainWindow.CheckProfileImageChange(FileName);

            CheckProfileImageChange();
        }

        private void CheckProfileImageChange()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == AuthenticationView.Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    if (user.UserData?.ProfilePhoto != FileName)
                    {
                        FileName = user.UserData?.ProfilePhoto;
                        profileImage.ImageSource = new BitmapImage(new Uri(FileName));
                    }
                }
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.CheckProfileImageChange(FileName);
        }
    }
}
