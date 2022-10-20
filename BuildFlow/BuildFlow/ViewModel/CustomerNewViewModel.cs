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
                LastName = LastName
            };

            if (Customer.InsertCustomer(newCustomer))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Customer successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Customer not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(FirstName) && !HasErrors;
    }
}
