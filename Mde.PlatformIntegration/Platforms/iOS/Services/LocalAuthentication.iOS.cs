using Mde.PlatformIntegration.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class LocalAuthentication : ILocalAuthentication
    {
        public Task<AuthenticationResult> PromptLoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
