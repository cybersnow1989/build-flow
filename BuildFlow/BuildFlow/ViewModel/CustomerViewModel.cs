using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BuildFlow.Model;
using BuildFlow.Services;
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

        public CustomerViewModel(INavService navService) : base(navService)
        {
            Customers = new ObservableCollection<Customer>();
        }

        public override void Init()
        {
            LoadCustomers();
        }

        void LoadCustomers()
        {
            Customers.Clear();

            Customers.Add(new Customer
            {
                FirstName = "FirstName1",
                LastName = "LastName1"
            });

            Customers.Add(new Customer
            {
                FirstName = "FirstName2",
                LastName = "LastName2"
            });
        }
    }
}
