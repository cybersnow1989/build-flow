using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
using BuildFlow.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailsPage : ContentPage
    {
        CustomerDetailsViewModel ViewModel => BindingContext as CustomerDetailsViewModel;
        public CustomerDetailsPage()
        {
            InitializeComponent();

            BindingContextChanged += CustomerDetailsPage_BindingContextChanged;

            BindingContext = new CustomerDetailsViewModel(DependencyService.Get<INavService>());
        }

        private void CustomerDetailsPage_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propertyHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(ViewModel.FirstName):
                    customerFirstNameEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.LastName):
                    customerLastNameEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}