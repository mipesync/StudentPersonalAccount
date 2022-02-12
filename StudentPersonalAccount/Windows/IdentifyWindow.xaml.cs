using Microsoft.Extensions.Logging;
using StudentPersonalAccount.Classes;
using StudentPersonalAccount.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для IdentifyWindow.xaml
    /// </summary>
    public partial class IdentifyWindow : Window
    {
        public IdentifyWindow()
        {
            InitializeComponent();
            mainControl.Content = new AuthenticationView(this);
            CheckObject();
        }

        private static bool closingState = false;
        public static object? ViewObject = "";

        private async void CheckObject()
        {
            while (true)
            {
                await Task.Delay(1);

                object? viewObject = ViewObject;

                switch (viewObject?.ToString())
                {
                    case "StudentPersonalAccount.View.AuthenticationView":
                        mainControl.Content = ViewObject;
                        break;
                    case "StudentPersonalAccount.View.RegistrationView":
                        mainControl.Content = ViewObject;
                        break;
                }

                if (closingState == true)
                {
                    break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closingState = true;
        }
    }
}
