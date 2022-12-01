using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Command SearchCommand => new Command(async () => await Search());

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public List<Customer> Customers { get; set; }

        private ObservableCollection<Customer> _customerList;

        public ObservableCollection<Customer> CustomerList
        {
            get => _customerList;
            set
            {
                _customerList = value;
                OnPropertyChanged();
            }
        }

        public CustomerViewModel(INavService navService) : base(navService)
        {
            CustomerList = new ObservableCollection<Customer>();
            Customers = new List<Customer>();
        }

        public override void Init()
        {
            LoadCustomers();
        }

        void LoadCustomers()
        {
            var customers = Customer.GetCustomers();
            Customers.Clear();
            CustomerList.Clear();

            foreach (Customer customer in customers)
            {
                Customers.Add(customer);
                CustomerList.Add(customer);
            }
        }

        async Task Search()
        {
            var results = Customers.Where(x => x.FirstName.ToLower().Contains(SearchText.ToLower())
                                               || x.LastName.ToLower().Contains(SearchText.ToLower())
                                               || x.Address.ToLower().Contains(SearchText.ToLower())
                                               || x.City.ToLower().Contains(SearchText.ToLower())
                                               || x.State.ToLower().Contains(SearchText.ToLower())
                                               || x.Email.ToLower().Contains(SearchText.ToLower())).ToList();

            CustomerList.Clear();

            foreach (var result in results)
            {
                CustomerList.Add(result);
            }
        }
    }
}
