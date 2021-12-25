using StudentPersonalAccount.Classes;
using StudentPersonalAccount.DBContext;
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
using System.Windows.Threading;

namespace StudentPersonalAccount.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
            this.Close();
        }

        private DispatcherTimer timer = new DispatcherTimer();
        private SetErrorProperty setErrorProperty = new SetErrorProperty();

        public static string? Login;
        public static string? Pass;

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                Login = loginTextBox.Text;
                Pass = passTextBox.Password;

                if (Login.Length < 1)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "Fill in the field!");
                }

                if (Pass.Length <= 0)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Fill in the field!");
                    goto Next;
                }
                else if (Pass.Length < 8)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Password less than 8 chars");
                    goto Next;
                }

                var users = context.Users.Where(p => p.Login == Login);

                string? _userId = null;

                foreach (var user in users)
                {
                    if (user.Password == Pass)
                    {
                        _userId = user.Id.ToString();
                        MainWindow mainWindow = new MainWindow();
                        Hide();
                        mainWindow.Show();
                        Close();
                    }
                    else setErrorProperty.SetProperty(passTextBox, passIcon, "Incorrect password!");
                    goto Next;
                }

                if (Login.Length > 0 && Pass.Length > 8 && _userId == null)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "User not found!");
                }
            }

            Next:
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            setErrorProperty.ClearProperty(loginTextBox, loginIcon);
            setErrorProperty.ClearProperty(passTextBox, passIcon);
            timer.Stop();
        }
    }
}
