using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow
{
    public class InvoiceNotProvidedException : Exception
    {
        public InvoiceNotProvidedException() : base(
            "An Invoice object was not provided. If using InvoiceDetailsViewModel, be sure to use the Init overload that takes a Invoice parameter.")
        {
        }
    }
}
