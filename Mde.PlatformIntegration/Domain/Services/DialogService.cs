using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mde.PlatformIntegration.Domain.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
        public Task ShowToast(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14)
        {
            return Toast.Make(message, duration, textSize).Show();
        }
    }
}
