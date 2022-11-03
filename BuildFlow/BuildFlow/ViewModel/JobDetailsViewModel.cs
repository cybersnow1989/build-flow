using BuildFlow.Model;
using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class JobDetailsViewModel : BaseValidationViewModel<Job>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        private Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));
        public Command AddInvoiceCommand => new Command<Job>(async job => await NavService.NavigateTo<InvoiceNewViewModel, Job>(job));
        public Command ViewInvoiceCommand => new Command<Invoice>(async invoice => await NavService.NavigateTo<InvoiceDetailsViewModel, Invoice>(invoice));

        private Job _selectedJob;

        public Job SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
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

        private Invoice _jobInvoice;

        public Invoice JobInvoice
        {
            get => _jobInvoice;
            set
            {
                _jobInvoice = value;
                OnPropertyChanged();
            }
        }

        private int _jobInvoiceID;

        public int JobInvoiceID
        {
            get => _jobInvoiceID;
            set
            {
                _jobInvoiceID = value;
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

        public JobDetailsViewModel(INavService navService) : base(navService)
        {
            CustomerList = new ObservableCollection<Customer>();
            Customers = new List<Customer>();
        }

        public override void Init(Job selectedJob)
        {
            LoadCustomers();
            SelectedJob = selectedJob;
            JobName = selectedJob.JobName;
            var jobCustomer = CustomerList.FirstOrDefault(x => x.ID == selectedJob.CustomerID);
            JobCustomerName = $"{jobCustomer.LastName}, {jobCustomer.LastName} ({jobCustomer.ID})";
            JobInvoice = Invoice.GetInvoiceByJobID(SelectedJob.ID);
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
            SelectedJob.JobName = JobName;

            if (Job.Update(SelectedJob))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Job successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Job not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task Delete()
        {
            if (Job.Delete(SelectedJob))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Job successfully deleted.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Job not deleted.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !string.IsNullOrWhiteSpace(JobName) && !HasErrors;
    }
}
