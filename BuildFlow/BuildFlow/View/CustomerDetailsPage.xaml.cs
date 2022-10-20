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

            BindingContext = new CustomerDetailsViewModel(DependencyService.Get<INavService>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CustomerDetailsViewModel.SelectedCustomer))
            {
                UpdateCustomer();
            }
        }

        private void UpdateCustomer()
        {
            if(ViewModel.SelectedCustomer == null) return;

            customerFirstNameEntry.Text = ViewModel.SelectedCustomer.FirstName;
            customerLastNameEntry.Text = ViewModel.SelectedCustomer.LastName;
        }
    }
}