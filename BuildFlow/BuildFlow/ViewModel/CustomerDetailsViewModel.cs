using System;
using System.Collections.Generic;
using System.Text;
using BuildFlow.Model;
using BuildFlow.Services;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerDetailsViewModel : BaseValidationViewModel<Customer>
    {
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
    }
}
