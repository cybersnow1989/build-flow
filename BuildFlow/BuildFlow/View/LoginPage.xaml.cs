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
        LoginViewModel ViewModel => BindingContext as LoginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContextChanged += LoginPage_BindingContextChanged;
            BindingContext = new LoginViewModel(DependencyService.Get<INavService>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.Init();
        }

        private void LoginPage_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propertyHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(ViewModel.Email):
                    emailEntry.BackgroundColor = propertyHasErrors ? Color.Red : Color.White;
                    break;
                case nameof(ViewModel.Password):
                    passwordEntry.BackgroundColor = propertyHasErrors ? Color.Red : Color.White;
                    break;
                default:
                    break;
            }
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
