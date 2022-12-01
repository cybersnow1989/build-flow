using BuildFlow.Model;
using BuildFlow.Services;
using BuildFlow.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class LoginViewModel : BaseValidationViewModel
    {
        private Command _loginCommand;
        public Command LoginCommand => _loginCommand ?? (_loginCommand = new Command(async () => await Login(), CanLogin));

        //private User _currentUser;

        //public User CurrentUser
        //{
        //    get => _currentUser;
        //    set
        //    {
        //        _currentUser = value;
        //        OnPropertyChanged();
        //    }
        //}

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Validate(() => !string.IsNullOrEmpty(_email), "Email must be provided.");
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Validate(() => !string.IsNullOrEmpty(_password), "Password must be provided.");
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        public LoginViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
            //TODO: Remove before app finalize
            Email = "test.contractor@test.com";
            Password = "test1234";
        }

        async Task Login()
        {
            var currentUser = User.GetUserByEmailAndPassword(Email, Password);

            if (currentUser != null)
            {
                CurrentUser.ID = currentUser.ID;
                CurrentUser.Email = currentUser.Email;
                CurrentUser.Password = currentUser.Password;
                CurrentUser.FirstName = currentUser.FirstName;
                CurrentUser.LastName = currentUser.LastName;
                CurrentUser.CompanyName = currentUser.CompanyName;
                CurrentUser.Address = currentUser.Address;
                CurrentUser.City = currentUser.City;
                CurrentUser.State = currentUser.State;
                CurrentUser.ZipCode = currentUser.ZipCode;
                await NavService.NavigateTo<HomeViewModel>();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Email or Password is incorrect.", "Ok");
            }
        }

        bool CanLogin() => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password) && !HasErrors;
    }
}
