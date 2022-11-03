﻿using BuildFlow.Services;
using BuildFlow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvoiceDetailsPage : ContentPage
    {
        public InvoiceDetailsPage()
        {
            InitializeComponent();

            BindingContext = new InvoiceDetailsViewModel(DependencyService.Get<INavService>());
        }
    }
}