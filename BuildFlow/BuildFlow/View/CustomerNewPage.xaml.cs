using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildFlow.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuildFlow.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerNewPage : ContentPage
    {
        public CustomerNewPage()
        {
            InitializeComponent();
        }

        private void Save_OnClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(customerFirstNameEntry.Text))
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = customerFirstNameEntry.Text
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Customer>();
                    int rowsAffected = conn.Insert(newCustomer);

                    if (rowsAffected > 0)
                    {
                        DisplayAlert("Success", "Customer saved", "Ok");
                    }
                    else
                        DisplayAlert("Failure", "Customer was not saved, please try again", "Ok");
                }

                Navigation.PushAsync(new HomePage()); 
            }
        }
    }
}