using Mde.PlatformIntegration.Domain.Services;
using Windows.Security.Credentials.UI;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class NativeAuthentication : INativeAuthentication
    {
        public bool IsSupported()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> PromptLoginAsync()
        {
            var result = await UserConsentVerifier.RequestVerificationAsync("Please verify your identity");
            if(result == UserConsentVerificationResult.Verified)
            {
                return new AuthenticationResult
                {
                    Authenticated = true
                };
            }
            else
            {
                return new AuthenticationResult
                {
                    Authenticated = false,
                    ErrorMessage = "Authentication failed"
                };
            }
        }
    }
}
