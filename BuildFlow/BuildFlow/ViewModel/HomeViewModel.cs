using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
        }
    }
}
