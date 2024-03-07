using CommunityToolkit.Mvvm.ComponentModel;
using Mde.PlatformIntegration.Domain.Services;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;
using System.Windows.Input;

namespace Mde.PlatformIntegration.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILocalAuthentication localAuthentication;

        public MainViewModel(ILocalAuthentication localAuthentication)
        {
            this.localAuthentication = localAuthentication;
        }

        public ICommand LoginCommand => new Command(async () =>
        {
            var result = await localAuthentication.PromptLoginAsync();
            if (!result.Authenticated)
            {
                await Shell.Current.GoToAsync("login", true);
            }
        }); 

    }

}
