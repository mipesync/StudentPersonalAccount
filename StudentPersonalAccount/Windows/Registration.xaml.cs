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
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            Authentication authentication = new Authentication();
            this.Hide();
            authentication.Show();
            this.Close();
        }

        private DispatcherTimer timer = new DispatcherTimer();

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            ColorErrorSet("Clear");
            using (var context = new UserContext())
            {
                var login = loginTextBox.Text;
                var pass = passTextBox.Password;
                var rePass = repeatPassTextBox.Password;
                var email = emailTextBox.Text;

                if (login.Length < 1)
                {
                    ColorErrorSet("NullLogin");
                }

                if (email.Length > 1)
                {
                    if (email.IndexOf('@') < 0 && email.IndexOf('.') < 0)
                    {

                    }
                }

                if (pass == rePass && pass.Length >= 8)
                {
                    var user = new User { Login = login, Password = pass, Email = email };
                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Done!");
                } else if (pass.Length < 8)
                {
                    ColorErrorSet("ShortPass");
                } else if (pass != rePass)
                {
                    ColorErrorSet("DifferentPass");
                }
            }

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            ColorErrorSet("Clear");
            timer.Stop();
        }

        private void ColorErrorSet(string state)
        {
            switch (state)
            {
                case "DifferentPass":
                    HintAssist.SetHelperText(passTextBox, "Different passwords!");
                    passIcon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    rePassIcon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    passTextBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    HintAssist.SetHelperText(repeatPassTextBox, "Different passwords!");
                    repeatPassTextBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    break;
                case "Clear":
                    passIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    rePassIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    passTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    repeatPassTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    loginIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    loginTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    break;
                case "ShortPass":
                    HintAssist.SetHelperText(passTextBox, "Password less than 8 char!");
                    passIcon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    passTextBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    break;
                case "NullLogin":
                    HintAssist.SetHelperText(loginTextBox, "Fill in the field!");
                    loginIcon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    loginTextBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    break;
                case "IncorrectEmail":
                    HintAssist.SetHelperText(emailTextBox, "Fill in the field!");
                    emailIcon.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    emailTextBox.Foreground = new SolidColorBrush(Color.FromRgb(183, 58, 58));
                    break;
            }
        }
    }
}
