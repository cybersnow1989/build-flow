using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BuildFlow.Model
{
    public class CustomerReport
    {
        public string ZipCode { get; set; }

        public ObservableCollection<Customer> CustomerList { get; set; }
    }
}
