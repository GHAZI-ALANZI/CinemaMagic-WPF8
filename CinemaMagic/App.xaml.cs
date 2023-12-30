﻿using CinemaMagic.Views;
using System.Windows;

namespace CinemaMagic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Assuming your startup window class is StartWindow
            LoginView window = new LoginView();
            window.Show();
        }


    }

}
