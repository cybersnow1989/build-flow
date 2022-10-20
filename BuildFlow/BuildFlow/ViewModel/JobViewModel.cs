using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow.ViewModel
{
    public class JobViewModel : BaseViewModel
    {
        public JobViewModel(INavService navService) : base(navService)
        {
        }
    }
}
