using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BuildFlow.Model;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public CustomerViewModel()
        {
            Customers = new ObservableCollection<Customer>
            {
                new Customer
                {
                    FirstName = "FirstName1",
                    LastName = "LastName1"
                },
                new Customer
                {
                    FirstName = "FirstName2",
                    LastName = "LastName2"
                },
                new Customer
                {
                    FirstName = "FirstName3",
                    LastName = "LastName3"
                }
            };
        }
    }
}
