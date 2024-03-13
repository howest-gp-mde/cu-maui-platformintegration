using Mde.PlatformIntegration.Domain.Models;
using System.Text.Json;

namespace Mde.PlatformIntegration.Domain.Services
{
    public class SecureProfileService(ISecureStorage secureStorage) : IProfileService
    {
        private const string ProfileKey = "profile";

        public async Task<Profile> GetProfileAsync()
        {
            var profileJson = await secureStorage.GetAsync(ProfileKey);
            if (profileJson != null)
            {
                return JsonSerializer.Deserialize<Profile>(profileJson);
            }
            return null;
        }

        public async Task SaveProfileAsync(Profile profile)
        {
            var profileJson = JsonSerializer.Serialize(profile);
            await secureStorage.SetAsync(ProfileKey, profileJson);
        }

        public Task DeleteProfileAsync(Profile profile)
        {
            secureStorage.Remove(ProfileKey);
            return Task.CompletedTask;
        }
    }   
}
