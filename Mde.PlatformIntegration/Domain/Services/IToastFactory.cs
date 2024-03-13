using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mde.PlatformIntegration.Domain.Services
{
    public interface IToastFactory
    {
        IToast CreateToast(string message, ToastDuration duration = ToastDuration.Short, double textSize = 14);
    }
}
