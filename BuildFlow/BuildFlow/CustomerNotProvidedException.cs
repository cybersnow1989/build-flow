using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow
{
    public class CustomerNotProvidedException : Exception
    {
        public CustomerNotProvidedException() : base(
            "A Customer object was not provided. If using CustomerDetailsViewModel, be sure to use the Init overload that takes a Customer parameter.")
        {
        }
    }
}
