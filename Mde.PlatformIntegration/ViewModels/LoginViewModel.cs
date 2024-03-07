﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.PlatformIntegration.Domain.Services;
using System.Windows.Input;

namespace Mde.PlatformIntegration.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly ILocalAuthentication localAuthentication;

        public LoginViewModel(ILocalAuthentication localAuthentication)
        {
            this.localAuthentication = localAuthentication;
        }

        public ICommand OnAppearingCommand => new Command(async () =>
        {
            
        });
    }

}
