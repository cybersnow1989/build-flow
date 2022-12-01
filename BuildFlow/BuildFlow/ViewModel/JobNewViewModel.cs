using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class JobNewViewModel : BaseValidationViewModel
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        public Command SelectCustomerCommand => new Command<Customer>(async customer => await SelectCustomer(customer));
        public Command SearchCommand => new Command(async () => await Search());


        #region Properties

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

        private string _jobTitle;

        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value;
                Validate(() => !string.IsNullOrWhiteSpace(_jobTitle), "Job title must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public List<Customer> Customers { get; set; }

        public Customer JobCustomer { get; set; }

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

        private string _jobCustomerName;

        public string JobCustomerName
        {
            get => _jobCustomerName;
            set
            {
                _jobCustomerName = value;
                Validate(() => !string.IsNullOrWhiteSpace(_jobCustomerName), "Customer must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
        #endregion

        public JobNewViewModel(INavService navService) : base(navService)
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

        async Task Save()
        {
            var newJob = new Job
            {
                JobTitle = JobTitle,
                CustomerID = JobCustomer.ID,
            };

            if (Job.InsertJob(newJob).ID != 0)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Job successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Job not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task SelectCustomer(Customer customer)
        {
            JobCustomer = customer;
            JobCustomerName = $"{customer.LastName}, {customer.FirstName} (ID: {customer.ID})";
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

        bool CanSave() => !string.IsNullOrWhiteSpace(JobTitle)
                          && !string.IsNullOrWhiteSpace(JobCustomerName)
                          && !HasErrors;
    }
}
