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

namespace StudentPersonalAccount.View
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationView.xaml
    /// </summary>
    public partial class AuthenticationView : UserControl
    {
        public AuthenticationView()
        {
            InitializeComponent();
        }

        public AuthenticationView(IdentifyWindow identify)
        {
            InitializeComponent();
            identifyWindow = identify;
        }

        IdentifyWindow? identifyWindow;

        private void regButton_Click(object sender, RoutedEventArgs e)
        {
            IdentifyWindow.ViewObject = new RegistrationView();
        }

        DispatcherTimer timer = new DispatcherTimer();
        private SetErrorProperty setErrorProperty = new SetErrorProperty();

        private string? Login;
        private string? Pass;

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                Login = loginTextBox.Text.Trim();
                Pass = passTextBox.Password.Trim();

                if (Login.Length < 1)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "Fill in the field!");
                }

                if (Pass.Length <= 0)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Fill in the field!");
                    TickStart();
                }
                else if (Pass.Length < 8)
                {
                    setErrorProperty.SetProperty(passTextBox, passIcon, "Password less than 8 chars");
                    TickStart();
                }

                var users = context.Users.Where(p => p.Login == Login);

                string? _userId = null;

                foreach (var user in users)
                {
                    var hash = user.Password;

                    if (EncryptionFactory.Create().Verify(Pass, hash))
                    {
                        _userId = user.Id.ToString();
                        MainWindow mainWindow = new MainWindow(Login);
                        mainWindow.Show();
                        this.identifyWindow?.Close();
                    }
                    else setErrorProperty.SetProperty(passTextBox, passIcon, "Incorrect password!");
                    TickStart();
                }

                if (Login.Length > 0 && Pass.Length > 8 && _userId == null)
                {
                    setErrorProperty.SetProperty(loginTextBox, loginIcon, "User not found!");
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
            setErrorProperty.ClearProperty(loginTextBox, loginIcon);
            setErrorProperty.ClearProperty(passTextBox, passIcon);
            timer.Stop();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                authButton_Click(sender, e);
            }
        }
    }
}
