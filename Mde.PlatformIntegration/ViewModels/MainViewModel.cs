using CommunityToolkit.Mvvm.ComponentModel;
using Mde.PlatformIntegration.Domain.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace Mde.PlatformIntegration.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly INativeAuthentication localAuthentication;

        public MainViewModel(INativeAuthentication localAuthentication)
        {
            this.localAuthentication = localAuthentication;
        }

        //public ICommand LoginCommand => new Command(async () =>
        //{
        //    if (localAuthentication.IsSupported())
        //    {
        //        var result = await localAuthentication.PromptLoginAsync();
        //        if (result.Authenticated)
        //        {
        //            Debug.WriteLine("Logged in");
        //            //await Shell.Current.GoToAsync("login", true);
        //        }
        //        else
        //        {
        //            Debug.WriteLine("Login failed");
        //        }
        //    }
        //    else
        //    {
        //        Debug.WriteLine("Local authentication is not supported on this platform");
        //    }
        //});

        public ICommand GoToProfileCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("profile", true);
        });
        public ICommand GoToAudioPlayerCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("audioplayer", true);
        });
        public ICommand GoToAudioRecorderCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("audiorecorder", true);
        });
    }

}
