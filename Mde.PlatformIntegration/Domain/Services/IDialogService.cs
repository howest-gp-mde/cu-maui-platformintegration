using CommunityToolkit.Maui.Core;

namespace Mde.PlatformIntegration.Domain.Services
{
    public interface IDialogService
    {
        Task ShowToast(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14);
        Task ShowAlertAsync(string title, string message, string cancel = "OK");
        Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No");

    }
}
