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

        public Command SelectCustomerCommand => new Command<Customer>(async customer => await SetCustomerID(customer));
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

        private string _jobName;

        public string JobName
        {
            get => _jobName;
            set
            {
                _jobName = value;
                Validate(() => !string.IsNullOrWhiteSpace(_jobName), "Job name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private int _customerID;

        public int CustomerID
        {
            get => _customerID;
            set
            {
                _customerID = value;
                Validate(() => Helpers.Validators.CheckIfZeroOrNegative(_customerID), "Customer must be selected.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
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
                JobName = JobName,
                CustomerID = CustomerID
            };

            if (Job.InsertJob(newJob))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Job successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Job not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(JobName) && !HasErrors;

        async Task SetCustomerID(Customer customer)
        {
            CustomerID = customer.ID;
        }

        async Task Search()
        {
            var results = Customers.Where(x => x.FirstName.ToLower().Contains(SearchText.ToLower()) || x.LastName.ToLower().Contains(SearchText.ToLower())).ToList();
            CustomerList.Clear();

            foreach (var result in results)
            {
                CustomerList.Add(result);
            }
        }
    }
}
