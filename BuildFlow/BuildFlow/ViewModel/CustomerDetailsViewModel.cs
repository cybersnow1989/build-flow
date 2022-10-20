using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerDetailsViewModel : BaseValidationViewModel<Customer>//BaseViewModel<Customer>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public CustomerDetailsViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init(Customer selectedCustomer)
        {
            SelectedCustomer = selectedCustomer;
        }

        async Task Save()
        {
            //Do something
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(SelectedCustomer.FirstName);
    }
}
