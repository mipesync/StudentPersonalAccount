using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StudentPersonalAccount.DBContext;
using StudentPersonalAccount.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

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
                var userAdd = context.Users.FirstOrDefault(p => p.Login == AuthenticationView.Login);

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
                        profileImage.ImageSource = new BitmapImage(new Uri($"Res/{FileName}", UriKind.Relative));
                    }
                }
            }

            MainWindow.FileName = FileName;
        }

        bool isEnter = false;

        private async void profileImageEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isEnter == false)
            {
                while (elipseImageChanger.Opacity < 1)
                {
                    await Task.Delay(1);

                    elipseImageChanger.Opacity += 0.10;
                }

                isEnter = true;
            }
        }

        private async void profileImageEllipse_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isEnter == true)
            {
                while (elipseImageChanger.Opacity > 0)
                {
                    await Task.Delay(1);

                    elipseImageChanger.Opacity -= 0.10;
                }

                isEnter = false;
            }
        }

        private void profileImageChange_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            FileName = openFileDialog.FileName;
            string uriFilename = Path.GetFileName(openFileDialog.FileName);

            FileInfo fileInfo = new FileInfo(FileName);
            fileInfo.CopyTo(@$"Res/{uriFilename}");


            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == AuthenticationView.Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    user.UserData.ProfilePhoto = uriFilename;
                }

                context.SaveChanges();
            }
            profileImage.ImageSource = new BitmapImage(new Uri($"Res/{uriFilename}", UriKind.Relative));

            MainWindow.FileName = FileName;

            CheckProfileImageChange();
        }
    }
}
