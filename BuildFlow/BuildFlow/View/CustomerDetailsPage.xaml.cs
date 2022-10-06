using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailsPage : ContentPage
    {
        CustomerDetailsViewModel ViewModel => BindingContext as CustomerDetailsViewModel;
        public CustomerDetailsPage(Customer selectedCustomer)
        {
            InitializeComponent();

            BindingContext = new CustomerDetailsViewModel(selectedCustomer);

            customerFirstNameEntry.Text = ViewModel.SelectedCustomer.FirstName;
            customerLastNameEntry.Text = ViewModel.SelectedCustomer.LastName;
        }
    }
}