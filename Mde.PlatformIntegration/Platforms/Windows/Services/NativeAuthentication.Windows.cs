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

        public async Task<AuthenticationResult> PromptLoginAsync(string prompt)
        {
            var result = await UserConsentVerifier.RequestVerificationAsync(prompt);
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
