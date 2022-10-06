using System;
using System.Collections.Generic;
using System.Text;
using BuildFlow.Model;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerDetailsViewModel : BaseViewModel
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

        public CustomerDetailsViewModel(Customer selectedCustomer)
        {
            SelectedCustomer = selectedCustomer;
        }
    }
}
