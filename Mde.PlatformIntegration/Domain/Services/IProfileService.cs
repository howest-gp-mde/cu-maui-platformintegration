using Mde.PlatformIntegration.Domain.Models;

namespace Mde.PlatformIntegration.Domain.Services
{
    public interface IProfileService
    {
        public Task<Profile> GetProfileAsync();

        public Task SaveProfileAsync(Profile profile);

        Task DeleteProfileAsync(Profile profile);
    }
}
