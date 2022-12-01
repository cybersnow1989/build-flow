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
    public class ProfileViewModel: BaseValidationViewModel<User>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        public Command ViewReportsCommand => new Command(async () => await NavService.NavigateTo<ReportsViewModel>());

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Validate(() => !string.IsNullOrWhiteSpace(_firstName), "First name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                Validate(() => !string.IsNullOrWhiteSpace(_lastName), "Last name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _companyName;

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                Validate(() => !string.IsNullOrWhiteSpace(_address), "Address must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _city;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                Validate(() => !string.IsNullOrWhiteSpace(_city), "City must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _state;

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                Validate(() => !string.IsNullOrWhiteSpace(_state), "State must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _zipCode;

        public string ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                Validate(() => !string.IsNullOrWhiteSpace(_zipCode), "Zip code must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public ProfileViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init(User currentUser)
        {
            FirstName = CurrentUser.FirstName;
            LastName = CurrentUser.LastName;
            CompanyName = CurrentUser.CompanyName;
            Address = CurrentUser.Address;
            City = CurrentUser.City;
            State = CurrentUser.State;
            ZipCode = CurrentUser.ZipCode;
        }

        async Task Save()
        {
            var currentUser = new User
            {
                ID = CurrentUser.ID,
                FirstName = FirstName,
                LastName = LastName,
                CompanyName = CompanyName,
                Address = Address,
                City = City,
                State = State,
                ZipCode = ZipCode
            };

            if (User.Update(currentUser))
            {
                await App.Current.MainPage.DisplayAlert("Success", "User Profile successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "User Profile not saved.", "Ok");
            }
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(FirstName) && !HasErrors;
    }
}
