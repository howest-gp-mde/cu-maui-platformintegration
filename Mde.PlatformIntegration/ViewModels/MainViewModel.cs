using CommunityToolkit.Mvvm.ComponentModel;
using Mde.PlatformIntegration.Domain.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace Mde.PlatformIntegration.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ISms smsService;

        public MainViewModel(ISms smsService)
        {
            this.smsService = smsService;
        }

        public bool IsSmsSupported => smsService.IsComposeSupported;

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
        public ICommand GoToSmsCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("sms", true);
        });
    }

}
