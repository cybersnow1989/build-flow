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

        public Command SelectLineItemCommand => new Command<LineItem>(async lineItem => await SelectLineItem(lineItem));
        public Command UpdateLineItemCommand => new Command(async () => await UpdateLineItem(SelectedLineItem));
        public Command DeleteLineItemCommand => new Command(async () => await DeleteLineItem(SelectedLineItem));


        #region Properties

        public Job InvoiceJob { get; set; }

        private LineItem _selectedLineItem;

        public LineItem SelectedLineItem
        {
            get => _selectedLineItem;
            set
            {
                _selectedLineItem = value;
                OnPropertyChanged();
            }
        }

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

        private string _itemPrice;
        public string ItemPrice
        {
            get => _itemPrice;
            set
            {
                _itemPrice = value;
                OnPropertyChanged();
            }
        }

        private bool _updateButtonEnabled;

        public bool UpdateButtonEnabled
        {
            get => _updateButtonEnabled;
            set
            {
                _updateButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _deleteButtonEnabled;

        public bool DeleteButtonEnabled
        {
            get => _deleteButtonEnabled;
            set
            {
                _deleteButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

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
                InvoiceTotal = InvoiceTotal,
                CreatedOn = DateTime.Now
            };

            var insertedInvoice = Invoice.InsertInvoice(newInvoice);

            foreach (LineItem lineItem in LineItems)
            {
                lineItem.InvoiceID = insertedInvoice.ID;
            }

            var insertedLineItems= LineItem.InsertLineItems(LineItems.ToList());

            bool insertLineItemsSuccess = false;

            foreach (LineItem insertedLineItem in insertedLineItems)
            {
                if (insertedLineItem.ID != 0)
                {
                    insertLineItemsSuccess = true;
                }
                else
                {
                    insertLineItemsSuccess = false;
                    break;
                }
            }

            if (insertedInvoice.ID != 0 && insertLineItemsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not saved.", "Ok");
            }

            await NavService.NavigateTo<JobDetailsViewModel, Job>(InvoiceJob);
        }

        async Task AddItem()
        {
            string errorMessage = string.Empty;
            bool isError = false;
            decimal itemPriceDecimal;

            Decimal.TryParse(ItemPrice, out itemPriceDecimal);

            if (string.IsNullOrWhiteSpace(ItemDescription))
            {
                errorMessage += "Item Description is required." + Environment.NewLine;
                isError = true;
            }

            if (itemPriceDecimal == 0m)
            {
                errorMessage += "Item price cannot be zero.";
                isError = true;
            }

            if (!isError)
            {
                var newLineItem = new LineItem
                {
                    ItemDescription = ItemDescription,
                    ItemPrice = itemPriceDecimal
                };
                LineItems.Add(newLineItem);
                InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
                ItemPrice = String.Empty;
                ItemDescription = string.Empty;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", errorMessage, "Ok");
            }
        }

        async Task SelectLineItem(LineItem lineItem)
        {

            SelectedLineItem = lineItem;
            ItemDescription = lineItem.ItemDescription;
            ItemPrice = lineItem.ItemPrice.ToString();
            UpdateButtonEnabled = true;
            DeleteButtonEnabled = true;
        }

        async Task UpdateLineItem(LineItem lineItem)
        {
            string errorMessage = string.Empty;
            bool isError = false;
            decimal itemPriceDecimal;

            Decimal.TryParse(ItemPrice, out itemPriceDecimal);

            if (string.IsNullOrWhiteSpace(ItemDescription))
            {
                errorMessage += "Item Description is required." + Environment.NewLine;
                isError = true;
            }

            if (itemPriceDecimal == 0m)
            {
                errorMessage += "Item price cannot be zero.";
                isError = true;
            }

            if (!isError)
            {
                int newIndex = LineItems.IndexOf(lineItem);
                LineItems.Remove(lineItem);

                var updateLineItem = new LineItem
                {
                    ItemDescription = ItemDescription,
                    ItemPrice = itemPriceDecimal
                };
                LineItems.Add(updateLineItem);
                int oldIndex = LineItems.IndexOf(updateLineItem);
                LineItems.Move(oldIndex, newIndex);
                InvoiceTotal = LineItems.Sum(x => x.ItemPrice);
                ItemPrice = string.Empty;
                ItemDescription = string.Empty;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", errorMessage, "Ok");
            }
        }

        async Task DeleteLineItem(LineItem lineItem)
        {
            LineItems.Remove(lineItem);
            UpdateButtonEnabled = false;
            DeleteButtonEnabled = false;
        }

        bool CanSave() => !Helpers.Validators.CheckIfZeroOrNegative(InvoiceTotal) && !HasErrors;
    }
}
