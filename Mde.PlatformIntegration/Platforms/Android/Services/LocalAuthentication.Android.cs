using Android.Content;
using AndroidX.Biometric;
using AndroidX.Fragment.App;
using Microsoft.Maui.ApplicationModel;
using Mde.PlatformIntegration.Domain.Services;
using Microsoft.Maui.Platform;
using Application = Android.App.Application;
using Org.Apache.Http.Client;
using System.Threading.Tasks;
using Java.Util.Concurrent;
using Android.App;
using System.Threading;
using Java.Lang;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class LocalAuthentication : INativeAuthentication
    {
        public bool IsSupported()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> PromptLoginAsync()
        {
            var taskCancellationSource = new CancellationTokenSource();
            var taskCompletionSource = new TaskCompletionSource<AuthenticationResult>();

            var manager = BiometricManager.From(Application.Context);

            var handler = new AuthenticationHandler(taskCompletionSource);


            var builder = new BiometricPrompt.PromptInfo.Builder()
                .SetTitle("Secret stuff")
                .SetConfirmationRequired(true)
                .SetDescription("Access to your secrets requires authentication");

            builder = builder.SetNegativeButtonText("Cancel");

            var info = builder.Build();
            var executor = Executors.NewSingleThreadExecutor();

            var activity = (FragmentActivity) Platform.CurrentActivity;
            using var dialog = new BiometricPrompt(activity, executor, handler);

            await using (var taskCancellation = taskCancellationSource.Token.Register(dialog.CancelAuthentication))
            {
                dialog.Authenticate(info);
            }
            
            return await taskCompletionSource.Task;
        }

        public class AuthenticationHandler : BiometricPrompt.AuthenticationCallback, IDialogInterfaceOnClickListener
        {
            private readonly TaskCompletionSource<AuthenticationResult> taskCompletionSource;

            public AuthenticationHandler(TaskCompletionSource<AuthenticationResult> taskCompletionSource)
            {
                this.taskCompletionSource = taskCompletionSource;
            }

            public override void OnAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result)
            {
                base.OnAuthenticationSucceeded(result);

                taskCompletionSource.SetResult(new AuthenticationResult
                {
                    Authenticated = true,
                    ErrorMessage = null
                });
            }

            public override void OnAuthenticationError(int errorCode, ICharSequence errorMessage)
            {
                base.OnAuthenticationError(errorCode, errorMessage);

                taskCompletionSource.SetResult(new AuthenticationResult
                {
                    Authenticated = false,
                    ErrorMessage = errorMessage.ToString()
                });
            }

            public override void OnAuthenticationFailed()
            {
                base.OnAuthenticationFailed();

                taskCompletionSource.SetResult(new AuthenticationResult
                {
                    Authenticated = false,
                    ErrorMessage = "Authentication failed. Please try again."
                });
            }
            public void OnClick(IDialogInterface dialog, int which)
            {
                taskCompletionSource.SetResult(new AuthenticationResult
                {
                    Authenticated = false,
                    ErrorMessage = "Authentication cancelled"
                });
            }
        }
        //private void SetResultSafe(FingerprintAuthenticationResult result)
        //{
        //    if (!(_taskCompletionSource.Task.IsCanceled || _taskCompletionSource.Task.IsCompleted || _taskCompletionSource.Task.IsFaulted))
        //    {
        //        _taskCompletionSource.SetResult(result);
        //    }
        //}
    }
}
