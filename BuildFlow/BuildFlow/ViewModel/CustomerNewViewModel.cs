using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using BuildFlow.Model;
using BuildFlow.Services;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerNewViewModel : BaseValidationViewModel
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

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
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Validate(() => !string.IsNullOrWhiteSpace(_email), "Email name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                Validate(() => !string.IsNullOrWhiteSpace(_address), "Address name must be provided.");
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
                Validate(() => !string.IsNullOrWhiteSpace(_city), "City name must be provided.");
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
                Validate(() => !string.IsNullOrWhiteSpace(_state), "State name must be provided.");
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
                Validate(() => !string.IsNullOrWhiteSpace(_zipCode), "Last name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public CustomerNewViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
        }

        async Task Save()
        {
            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Address = Address,
                City = City,
                State = State,
                ZipCode = ZipCode
            };

            if (Customer.InsertCustomer(newCustomer).ID != 0)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Customer successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Customer not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(FirstName)
                          && !string.IsNullOrWhiteSpace(LastName)
                          && !string.IsNullOrWhiteSpace(Email)
                          && !string.IsNullOrWhiteSpace(Address)
                          && !string.IsNullOrWhiteSpace(City)
                          && !string.IsNullOrWhiteSpace(State)
                          && !string.IsNullOrWhiteSpace(ZipCode)
                          && !HasErrors;
    }
}
