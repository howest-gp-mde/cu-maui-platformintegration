using Mde.PlatformIntegration.Domain.Services;
using Windows.Security.Credentials.UI;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class LocalAuthentication : ILocalAuthentication
    {
        public async Task<AuthenticationResult> PromptLoginAsync()
        {
            var result = await UserConsentVerifier.RequestVerificationAsync("Please verify your identity");

            return result == UserConsentVerificationResult.Verified;
        }
    }
}
