using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerDetailsViewModel : BaseValidationViewModel<Customer>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        private Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));


        #region Properties
        public Customer SelectedCustomer { get; set; }

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
        #endregion

        public CustomerDetailsViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
            throw new CustomerNotProvidedException();
        }

        public override void Init(Customer selectedCustomer)
        {
            SelectedCustomer = selectedCustomer;
            FirstName = selectedCustomer.FirstName;
            LastName = selectedCustomer.LastName;
            Email = selectedCustomer.Email;
            Address = selectedCustomer.Address;
            City = selectedCustomer.City;
            State = selectedCustomer.State;
            ZipCode = selectedCustomer.ZipCode;
        }

        async Task Save()
        {
            SelectedCustomer.FirstName = FirstName;
            SelectedCustomer.LastName = LastName;

            if (Customer.Update(SelectedCustomer))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Customer successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Customer not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task Delete()
        {
            if (Customer.Delete(SelectedCustomer))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Customer successfully deleted.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Customer not deleted.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(FirstName) && !HasErrors;
    }
}
