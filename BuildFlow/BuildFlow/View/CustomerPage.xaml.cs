﻿using System;
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
    public partial class CustomerPage : ContentPage
    {
        public CustomerPage()
        {
            InitializeComponent();
            BindingContext = new CustomerViewModel();
        }

        private void Customers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCustomer = (Customer) e.CurrentSelection.FirstOrDefault();

            if (selectedCustomer != null)
            {
                Navigation.PushAsync(new CustomerDetailsPage(selectedCustomer));
            }

            customers.SelectedItem = null;
        }
    }
}