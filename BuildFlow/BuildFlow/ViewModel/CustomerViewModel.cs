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
        public Command AddCommand => new Command(async () => await NavService.NavigateTo<CustomerNewViewModel>());
        public Command ViewCommand => new Command<Customer>(async customer => await NavService.NavigateTo<CustomerDetailsViewModel, Customer>(customer));

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
            var customers = Customer.GetCustomers();
            Customers.Clear();

            foreach (Customer customer in customers)
            {
                Customers.Add(customer);
            }
        }
    }
}
