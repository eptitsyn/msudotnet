﻿using System.Windows;

namespace task14
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            MainViewModel viewModel = new MainViewModel(mainWindow);
            mainWindow.DataContext = viewModel;
            MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
