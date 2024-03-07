﻿namespace Mde.PlatformIntegration.Domain.Services
{
    public interface INativeAuthentication
    {
        bool IsSupported();

        Task<AuthenticationResult> PromptLoginAsync();
    }

    public class AuthenticationResult
    {
        public bool Authenticated { get; set; }

        public string ErrorMessage { get; set; }
    }

}