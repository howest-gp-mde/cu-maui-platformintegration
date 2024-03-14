using Mde.PlatformIntegration.Domain.Services;
using Windows.Security.Credentials.UI;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class NativeAuthentication : INativeAuthentication
    {
        public bool IsSupported()
        {
            //var result = UserConsentVerifier.CheckAvailabilityAsync()
            //    .AsTask()
            //    .GetAwaiter()   //run synchronously because interface method as not a Task<>
            //    .GetResult();

            //return result == UserConsentVerifierAvailability.Available;
            return false;
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
