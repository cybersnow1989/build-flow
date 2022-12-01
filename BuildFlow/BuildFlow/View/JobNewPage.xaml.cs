using BuildFlow.Services;
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
    public partial class JobNewPage : ContentPage
    {
        JobNewViewModel ViewModel => BindingContext as JobNewViewModel;
        public JobNewPage()
        {
            InitializeComponent();

            BindingContextChanged += JobNewPage_BindingContextChanged;

            BindingContext = new JobNewViewModel(DependencyService.Get<INavService>());
        }

        private void JobNewPage_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propertyHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(ViewModel.JobTitle):
                    jobTitleLabel.TextColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.JobCustomerName):
                    customerTitleLabel.TextColor = propertyHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}