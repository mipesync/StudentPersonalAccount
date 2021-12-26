﻿using Microsoft.Extensions.Logging;
using StudentPersonalAccount.Classes;
using StudentPersonalAccount.MVVM.View;
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
            mainControl.Content = new RegistrationView();
            CheckedObject();
        }

        private static bool closingState = false;
        public static object? ViewObject = "";

        private async void CheckedObject()
        {
            while (true)
            {
                await Task.Delay(1);

                object? viewObject = ViewObject;
                object? authViewObj = new AuthenticationView(),
                        regViewObj = new RegistrationView();

                switch (viewObject?.ToString())
                {
                    case "StudentPersonalAccount.MVVM.View.AuthenticationView":
                        mainControl.Content = ViewObject;
                        break;
                    case "StudentPersonalAccount.MVVM.View.RegistrationView":
                        mainControl.Content = ViewObject;
                        break;
                }

                if (closingState == true)
                {
                    break;
                }

                MainWindow mainWindow = new MainWindow();
                if (mainWindow.IsActive)
                {
                    Environment.Exit(0);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closingState = true;
        }
    }
}
