using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BuildFlow.Model
{
    public class InvoiceReport
    {
        public string Customer { get; set; }

        public ObservableCollection<Invoice> InvoiceList { get; set; }
    }
}
