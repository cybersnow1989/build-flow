﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using BuildFlow.Services;
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

            BindingContext = new CustomerNewViewModel(DependencyService.Get<INavService>());
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
                case nameof(ViewModel.Email):
                    customerEmailEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.Address):
                    customerAddressEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.City):
                    customerCityEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.State):
                    customerStateEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.ZipCode):
                    customerZipCodeEntry.LabelColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}