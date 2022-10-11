using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.ViewModel;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerNewPage : ContentPage
    {
        CustomerNewViewModel ViewModel => BindingContext as CustomerNewViewModel;
        public CustomerNewPage()
        {
            InitializeComponent();

            BindingContextChanged += CustomerNewPage_BindingContextChanged;

            BindingContext = new CustomerNewViewModel();
        }

        private void CustomerNewPage_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
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

        //private void Save_OnClicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(customerFirstNameEntry.Text))
        //    {
        //        Customer newCustomer = new Customer()
        //        {
        //            FirstName = customerFirstNameEntry.Text
        //        };

        //        using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
        //        {
        //            conn.CreateTable<Customer>();
        //            int rowsAffected = conn.Insert(newCustomer);

        //            if (rowsAffected > 0)
        //            {
        //                DisplayAlert("Success", "Customer saved", "Ok");
        //            }
        //            else
        //                DisplayAlert("Failure", "Customer was not saved, please try again", "Ok");
        //        }

        //        Navigation.PushAsync(new HomePage()); 
        //    }
        //}
    }
}