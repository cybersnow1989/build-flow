using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Services;
using BuildFlow.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsPage : ContentPage
    {
        ReportsViewModel ViewModel => BindingContext as ReportsViewModel;
        public ReportsPage()
        {
            InitializeComponent();

            BindingContext = new ReportsViewModel(DependencyService.Get<INavService>());
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    ViewModel?.Init();
        //}
    }
}