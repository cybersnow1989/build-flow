using BuildFlow.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BuildFlow.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand => new Command(async () => await NavService.NavigateTo<HomeViewModel>());
        public LoginViewModel(INavService navService) : base(navService)
        {
        }
    }
}
