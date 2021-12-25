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
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new UserContext())
            {
                var usersAdd = context.Users.Where(p => p.Login == Authentication.Login);

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

                var usersGet = context.Users.Where(p => p.Login == Authentication.Login);

                User userGet = new User();

                foreach (var _user in usersGet)
                {
                    MessageBox.Show($"{_user.Id}. {_user.Login} - {_user.Password}\n" +
                        $"{_user.UserData?.SecondName} {_user.UserData?.FirstName} {_user.UserData?.LastName}\n" +
                        $"{_user.UserData?.Phone} {_user.UserData?.GroupNumber} {_user.Email}");
                }
            }
        }
    }
}
