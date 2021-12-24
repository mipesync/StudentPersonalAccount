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

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var login = loginTextBox.Text;
                var pass = passTextBox.Password;

                var users = context.Users.Where(p => p.Login == login);

                foreach (var user in users)
                {
                    if (user.Password == pass)
                    {
                        MessageBox.Show($"{user.Id}. {user.Login} {user.Password}");
                    }
                }
            }
        }
    }
}
