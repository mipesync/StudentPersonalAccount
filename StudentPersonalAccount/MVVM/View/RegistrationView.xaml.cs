using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using StudentPersonalAccount.Classes;
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
using System.Windows.Threading;

namespace StudentPersonalAccount.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
            textBoxes.Add(loginTextBox);
            textBoxes.Add(emailTextBox);
            passwordBoxes.Add(passTextBox);
            passwordBoxes.Add(repeatPassTextBox);
            icons.Add(loginIcon);
            icons.Add(passIcon);
            icons.Add(rePassIcon);
            icons.Add(emailIcon);
        }

        private DispatcherTimer timer = new DispatcherTimer();
        private static List<TextBox> textBoxes = new List<TextBox>();
        private static List<PasswordBox> passwordBoxes = new List<PasswordBox>();
        private static List<PackIcon> icons = new List<PackIcon>();

        private SetErrorProperty setErrorProperty = new SetErrorProperty();

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            IdentifyWindow.ViewObject = new AuthenticationView();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var login = loginTextBox.Text.Trim();
                var pass = passTextBox.Password.Trim();
                var hash = BCrypt.Net.BCrypt.HashPassword(pass);
                var rePass = repeatPassTextBox.Password.Trim();
                var email = emailTextBox.Text.Trim();

                var _users = context.Users.Where(p => p.Login == login);

                User userData = new User();

                if (login.Length < 1)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "Fill in the field!");
                }

                foreach (var _user in _users)
                {
                    if (_user.Login == login)
                    {
                        setErrorProperty.SetProperty(loginTextBox, loginIcon, "There is already such a user!");
                        TickStart();
                    }
                }

                if (email.Length > 1)
                {
                    if (email.IndexOf('@') < 1 || email.IndexOf('.') < 0)
                    {
                        setErrorProperty.SetProperty(emailTextBox, emailIcon, "Incorrect email!");
                    }
                }
                else setErrorProperty.SetProperty(emailTextBox, emailIcon, "Fill in the field!");

                if (pass == rePass && pass.Length >= 8)
                {
                    var user = new User { Login = login, Password = hash, Email = email};
                    context.Users.Add(user);


                    var uData = new UserData { FirstName = "Guest", ProfilePhoto = "user.png", 
                                                User = user};
                    context.UserDatas.Add(uData);
                    context.SaveChanges();
                    MessageBox.Show("Done!");
                }
                else if (pass.Length <= 0)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Fill in the field!");
                }
                else if (pass.Length < 8)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Password less than 8 chars");
                }
                else if (rePass.Length <= 0)
                {
                    setErrorProperty.SetProperty(repeatPassTextBox, rePassIcon, "Fill in the field!");
                    TickStart();
                }
                else if (pass != rePass)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Different passwords!");
                    setErrorProperty.SetProperty(repeatPassTextBox, rePassIcon, "Different passwords!");
                }
            }
        }

        private void TickStart()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            setErrorProperty.ClearProperty(textBoxes, icons, passwordBoxes);
            timer.Stop();
        }
    }
}
