using BuildFlow.Services;
using BuildFlow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        HomeViewModel ViewModel => BindingContext as HomeViewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(DependencyService.Get<INavService>());
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.Init();
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}