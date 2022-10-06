using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow
{
    public partial class App : Application
    {
        public static string DatabaseLocation = String.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.LoginPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.LoginPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
