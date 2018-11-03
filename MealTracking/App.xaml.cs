using System;
using System.Windows;
using MealTracking.Config;
using MealTracking.Pages;
using Unity;

namespace MealTracking
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = new UnityContainer();
            Bootstrap.Register(container);
            var mainWindow = container.Resolve<MainWindow>();

            if (mainWindow == null)
            {
                throw new Exception("Main window was not resolved");
            }  
            
            mainWindow.Title = "Meal tracking";
            
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}
