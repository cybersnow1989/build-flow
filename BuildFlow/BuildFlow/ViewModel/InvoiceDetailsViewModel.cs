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
    public class InvoiceDetailsViewModel : BaseValidationViewModel<Invoice>
    {
        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));
        private Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));

        public Invoice SelectedInvoice { get; set; }
        private decimal _invoiceTotal;

        public decimal InvoiceTotal
        {
            get => _invoiceTotal;
            set
            {
                _invoiceTotal = value;
                Validate(() => Helpers.Validators.CheckIfZeroOrNegative(_invoiceTotal), "Invoice total must be greater than 0.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public InvoiceDetailsViewModel(INavService navService) : base(navService)
        {
            SelectedInvoice = new Invoice();
        }

        public override void Init(Invoice selectedInvoice)
        {
        }

        async Task Save()
        {
            SelectedInvoice.InvoiceTotal = InvoiceTotal;

            if (Invoice.Update(SelectedInvoice))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully saved.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not saved.", "Ok");
            }

            await NavService.GoBack();
        }

        async Task Delete()
        {
            if (Invoice.Delete(SelectedInvoice))
            {
                await App.Current.MainPage.DisplayAlert("Success", "Invoice successfully deleted.", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failure", "Invoice not deleted.", "Ok");
            }

            await NavService.GoBack();
        }

        bool CanSave() => !HasErrors;
    }
}
