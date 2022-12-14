using System;
using BuildFlow.Services;
using BuildFlow.View;
using BuildFlow.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow
{
    public partial class App : Application
    {
        public static string DatabaseLocation = String.Empty;
        public App(string databaseLocation)
        {
            InitializeComponent();

            var loginPage = new NavigationPage(new LoginPage());
            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = loginPage.Navigation;
            navService.RegisterViewMapping(typeof(LoginViewModel), typeof(LoginPage));
            navService.RegisterViewMapping(typeof(HomeViewModel), typeof(HomePage));
            navService.RegisterViewMapping(typeof(CustomerViewModel), typeof(CustomerPage));
            navService.RegisterViewMapping(typeof(CustomerNewViewModel), typeof(CustomerNewPage));
            navService.RegisterViewMapping(typeof(CustomerDetailsViewModel), typeof(CustomerDetailsPage));

            MainPage = loginPage;

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
