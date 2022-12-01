using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow
{
    public class JobNotProvidedException : Exception
    {
        public JobNotProvidedException() : base(
            "A Job object was not provided. If using JobDetailsViewModel, be sure to use the Init overload that takes a Job parameter.")
        {
        }
    }
}
