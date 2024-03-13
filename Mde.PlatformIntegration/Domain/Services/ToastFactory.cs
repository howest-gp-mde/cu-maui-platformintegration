using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mde.PlatformIntegration.Domain.Services
{
    public class ToastFactory : IToastFactory
    {
        public IToast CreateToast(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14)
        {
            return Toast.Make(message, duration, textSize);
        }

    }
}
