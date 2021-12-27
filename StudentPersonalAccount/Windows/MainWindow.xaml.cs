using StudentPersonalAccount.DBContext;
using StudentPersonalAccount.MVVM.View;
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

namespace StudentPersonalAccount.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool OpenState = false;

        public MainWindow()
        {
            InitializeComponent();
            OpenState = true;
            mainControl.Content = new HomeView();
            using (var context = new UserContext())
            {
                var users = context.Users.Where(p => p.Login == "lolik123");

                foreach (var user in users)
                {
                    var sdsd = user.UserData?.SecondName;
                    FSNameLabel.Content = $"{user.UserData?.SecondName} {user.UserData?.FirstName}";
                    title.Content = sdsd;
                }

            }
            GetUser();
        }

        private void GetUser()
        {
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new ProfileView();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new HomeView();
        }
    }
}
