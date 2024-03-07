using Intents;
using Mde.PlatformIntegration.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class LocalAuthentication : INativeAuthentication
    {
        public bool IsSupported()
        {
            return false;
        }

        public Task<AuthenticationResult> PromptLoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
