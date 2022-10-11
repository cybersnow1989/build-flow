using System;
using System.Collections.Generic;
using System.Text;
using BuildFlow.Model;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerNewViewModel : BaseValidationViewModel
    {
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
                //Validate(() => !string.IsNullOrWhiteSpace(_lastName), "Last name must be provided.");
                Validate(() => _lastName == "Test", "Last name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public CustomerNewViewModel() {}

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));

        void Save()
        {
            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName
            };
            
            //TODO: Persist entry
        }

        //bool CanSave() => !string.IsNullOrWhiteSpace(FirstName) && !HasErrors;
        bool CanSave() => !HasErrors;
    }
}
