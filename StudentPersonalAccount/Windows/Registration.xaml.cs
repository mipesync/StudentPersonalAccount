using MaterialDesignThemes.Wpf;
using StudentPersonalAccount.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using StudentPersonalAccount.Classes;

namespace StudentPersonalAccount.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
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

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            Authentication authentication = new Authentication();
            this.Hide();
            authentication.Show();
            this.Close();
        }

        private DispatcherTimer timer = new DispatcherTimer();
        private static List<TextBox> textBoxes = new List<TextBox>();
        private static List<PasswordBox> passwordBoxes = new List<PasswordBox>();
        private static List<PackIcon> icons = new List<PackIcon>();

        private SetErrorProperty setErrorProperty = new SetErrorProperty();

        private void regButton_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new UserContext())
            {
                var login = loginTextBox.Text;
                var pass = passTextBox.Password;
                var rePass = repeatPassTextBox.Password;
                var email = emailTextBox.Text;

                if (login.Length < 1)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "Fill in the field!");
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
                    var user = new User { Login = login, Password = pass, Email = email };
                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Done!");
                } else if (pass.Length <= 0)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Fill in the field!");
                } else if (pass.Length < 8)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Password less than 8 chars");
                } else if (pass != rePass)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Different passwords!");
                    setErrorProperty.SetProperty(repeatPassTextBox, rePassIcon, "Different passwords!");
                }

                if (rePass.Length <= 0)
                {
                    setErrorProperty.SetProperty(repeatPassTextBox, rePassIcon, "Fill in the field!");
                }
            }

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
