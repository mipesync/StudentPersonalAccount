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

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var login = loginTextBox.Text;
                var pass = passTextBox.Password;

                if (login.Length < 1)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "Fill in the field!");
                }

                if (pass.Length <= 0)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Fill in the field!");
                }
                else if (pass.Length < 8)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Password less than 8 chars");
                }

                var users = context.Users.Where(p => p.Login == login);

                foreach (var user in users)
                {
                    if (user.Login == null && user.Password == null && user.Email == null)
                    {
                        MessageBox.Show("Ты ебалай?");
                    }
                    if (user.Password == pass)
                    {
                        MessageBox.Show($"{user.Id}. {user.Login} {user.Password} {user.Email}");
                    }
                }
            }

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
