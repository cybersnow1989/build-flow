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
    public class ReportsViewModel : BaseViewModel
    {
        public Command CustomersByZipReport => new Command(async () => await LoadCustomerReport());
        public Command InvoicesByCustomerReport => new Command(async () => await LoadInvoiceReport());

        private ObservableCollection<CustomerReport> _customerReportList;

        public ObservableCollection<CustomerReport> CustomerReportList
        {
            get => _customerReportList;
            set
            {
                _customerReportList = value;
                OnPropertyChanged();
            }
        }

        private bool _customerReportVisible;

        public bool CustomerReportVisible
        {
            get => _customerReportVisible;
            set
            {
                _customerReportVisible = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<InvoiceReport> _invoiceReportList;

        public ObservableCollection<InvoiceReport> InvoiceReportList
        {
            get => _invoiceReportList;
            set
            {
                _invoiceReportList = value;
                OnPropertyChanged();
            }
        }

        private bool _invoiceReportVisible;

        public bool InvoiceReportVisible
        {
            get => _invoiceReportVisible;
            set
            {
                _invoiceReportVisible = value;
                OnPropertyChanged();
            }
        }

        public ReportsViewModel(INavService navService) : base(navService)
        {
            CustomerReportList = new ObservableCollection<CustomerReport>();
            InvoiceReportList = new ObservableCollection<InvoiceReport>();
        }

        public override void Init()
        {
        }

        async Task LoadCustomerReport()
        {
            var zipCodes = Customer.GetCustomers().Select(x => x.ZipCode).Distinct().ToList();

            foreach (var zipCode in zipCodes)
            {
                var customerReportItem = new CustomerReport();
                customerReportItem.ZipCode = zipCode;
                var customerList = Customer.GetCustomers().Where(x => x.ZipCode == zipCode).ToList();
                customerReportItem.CustomerList = new ObservableCollection<Customer>(customerList);
                CustomerReportList.Add(customerReportItem);
            }

            InvoiceReportVisible = false;
            CustomerReportVisible = true;
        }

        async Task LoadInvoiceReport()
        {
            var customers = Customer.GetCustomers();

            foreach (var customer in customers)
            {
                var invoiceReportItem = new InvoiceReport();
                invoiceReportItem.Customer = $"{customer.LastName}, {customer.FirstName}";

                var jobs = Job.GetJobs().Where(x => x.CustomerID == customer.ID);
                var invoices = Invoice.GetInvoices().Where(i => jobs.Any(j => j.ID == i.JobID)).ToList();
                invoiceReportItem.InvoiceList = new ObservableCollection<Invoice>(invoices);
                InvoiceReportList.Add(invoiceReportItem);
            }

            InvoiceReportVisible = true;
            CustomerReportVisible = false;
        }
    }
}
