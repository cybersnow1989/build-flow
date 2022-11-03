using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Enums;
using BuildFlow.Model;
using BuildFlow.Services;
using BuildFlow.View;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class InvoiceNewViewModel : BaseValidationViewModel<Job>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        public Command AddItemCommand => new Command(async () => await AddItem());


        public Job InvoiceJob { get; set; }

        private ObservableCollection<LineItem> _lineItems;

        public ObservableCollection<LineItem> LineItems
        {
            get => _lineItems;
            set
            {
                _lineItems = value;
                OnPropertyChanged();
            }
        }

        private decimal _invoiceTotal;

        public decimal InvoiceTotal
        {
            get => _invoiceTotal;
            set
            {
                _invoiceTotal = value;
                OnPropertyChanged();
            }
        }

        private string _itemDescription;

        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value;
                OnPropertyChanged();
            }
        }

        private decimal _itemPrice;

        public decimal ItemPrice
        {
            get => _itemPrice;
            set
            {
                _itemPrice = value;
                OnPropertyChanged();
            }
        }

        public InvoiceNewViewModel(INavService navService) : base(navService)
        {
            InvoiceJob = new Job();
            LineItems = new ObservableCollection<LineItem>();
        }

        public override void Init(Job job)
        {
            InvoiceJob = job;
        }

        async Task Save()
        {
            var newInvoice = new Invoice
            {
                JobID = InvoiceJob.ID,
                InvoiceType = InvoiceType.Regular,
                InvoiceTotal = InvoiceTotal
            };

            bool insertInvoiceSuccess = Invoice.InsertInvoice(newInvoice);

            var insertedInvoice = Invoice.GetInvoiceByJobID(newInvoice.JobID);

            foreach (LineItem lineItem in LineItems)
            {
                lineItem.InvoiceID = insertedInvoice.ID;
            }

            bool insertLineItemsSuccess = LineItem.InsertLineItems(LineItems.ToList());

            if (insertInvoiceSuccess && insertLineItemsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !HasErrors;

        async Task AddItem()
        {
            var newLineItem = new LineItem
            {
                ItemDescription = ItemDescription,
                ItemPrice = ItemPrice
            };
            LineItems.Add(newLineItem);
            InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
            ItemPrice = 0;
            ItemDescription = string.Empty;
        }
    }
}
