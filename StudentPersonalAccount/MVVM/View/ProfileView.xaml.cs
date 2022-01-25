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
        public ProfileView(string login)
        {
            InitializeComponent();
            Login = login;
            textBoxes.Add(secondNameTextBox);
            textBoxes.Add(firstNameTextBox);
            textBoxes.Add(lastNameTextBox);
            textBoxes.Add(groupNumberTextBox);
            textBoxes.Add(phoneTextBox);
            CheckProfileImageChange();
            fillPersonInfo();
        }

        private string Login;

        private string? FileName;

        List<TextBox> textBoxes = new List<TextBox>();

        private void fillPersonInfo()
        {
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    string[] userData = new string[5] { user.UserData?.SecondName, user.UserData?.FirstName,
                         user.UserData?.LastName, user.UserData?.GroupNumber, user.UserData?.Phone };

                    for (int i = 0; i < 5; i++)
                    {
                        if (userData[i] != null && userData[i] != "Guest") { textBoxes[i].Text = userData[i]; }
                    }
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == Login);

                foreach (var userAdd in users.Include(p => p.UserData))
                {

                    string secondName = secondNameTextBox.Text.Trim(),
                    firstName = firstNameTextBox.Text.Trim(),
                    lastName = lastNameTextBox.Text.Trim(),
                    groupNumber = groupNumberTextBox.Text.Trim(),
                    phone = phoneTextBox.Text.Trim();

                    var userData = new UserData
                    {
                        FirstName = firstName,
                        SecondName = secondName,
                        LastName = lastName,
                        GroupNumber = groupNumber,
                        Phone = phone,
                        ProfilePhoto = FileName,
                        User = userAdd
                    };

                    context.UserDatas.Add(userData);
                    context.SaveChanges();
                }
            }

            foreach (var elem in textBoxes)
            {
                elem.IsEnabled = false;
            }
        }

        private void CheckProfileImageChange()
        {
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
                var users = context.Users.Where(p => p.Login == Login);

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

        private void profileImageDelete_Click(object sender, RoutedEventArgs e)
        {
            string? oldImage = string.Empty;

            using (var context = new UserContext())
            { 
                var users = context.Users.Where(p => p.Login == Login);

                foreach (var user in users.Include(p => p.UserData))
                {
                    oldImage = user.UserData?.ProfilePhoto;
                    user.UserData.ProfilePhoto = "user.png";
                }

                context.SaveChanges();
            }
            profileImage.ImageSource = new BitmapImage(new Uri($"Res/user.png", UriKind.Relative));

            MainWindow.FileName = "user.png";

            CheckProfileImageChange();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var elem in textBoxes)
            {
                elem.IsEnabled = true;
            }
        }
    }
}
