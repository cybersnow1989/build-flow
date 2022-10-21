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

        public CustomerDetailsViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init(Customer selectedCustomer)
        {
            SelectedCustomer = selectedCustomer;
            FirstName = selectedCustomer.FirstName;
            LastName = selectedCustomer.LastName;
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
