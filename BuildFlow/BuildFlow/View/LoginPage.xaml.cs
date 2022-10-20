using BuildFlow.Services;
using BuildFlow.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuildFlow.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(DependencyService.Get<INavService>());
        }

        //private void LoginButton_Clicked(object sender, EventArgs e)
        //{
        //    //TODO: Uncomment before publishing
        //    //bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
        //    //bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

        //    //if (isEmailEmpty || isPasswordEmpty)
        //    //{
        //        //TODO: Add error code popup for login
        //    //}
        //    //else
        //    //{
        //    //    Navigation.PushAsync(new HomePage());
        //    //}

        //    Navigation.PushAsync(new HomePage());
        //}
    }
}
