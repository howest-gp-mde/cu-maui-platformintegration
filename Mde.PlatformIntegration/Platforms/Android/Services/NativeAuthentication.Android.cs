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
using Android.OS;
using Android;
using Android.Content.PM;

namespace Mde.PlatformIntegration.Platforms.Services
{
    public class NativeAuthentication : INativeAuthentication
    {
        private readonly BiometricManager manager;

        public NativeAuthentication()
        {
            manager = BiometricManager.From(Application.Context);
        }

        public bool IsSupported()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
                return false;

            var context = Application.Context;

            if (context.CheckCallingOrSelfPermission(Manifest.Permission.UseBiometric) != Permission.Granted &&
                context.CheckCallingOrSelfPermission(Manifest.Permission.UseFingerprint) != Permission.Granted)
                return false;

            int result = manager.CanAuthenticate(BiometricManager.Authenticators.DeviceCredential | BiometricManager.Authenticators.BiometricWeak);
            return result == 0;
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

            //dialog.Authenticate(info);

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
                SetResult(true, null);
            }

            public override void OnAuthenticationError(int errorCode, ICharSequence errorMessage)
            {
                base.OnAuthenticationError(errorCode, errorMessage);
                SetResult(false, errorMessage.ToString());
            }

            public override void OnAuthenticationFailed()
            {
                base.OnAuthenticationFailed();
                SetResult(false, "Authentication failed. Please try again.");
            }
            public void OnClick(IDialogInterface dialog, int which)
            {
                SetResult(false, "Authentication cancelled");
            }

            private void SetResult(bool success, string errorMessage)
            {
                if (!(taskCompletionSource.Task.IsCanceled || taskCompletionSource.Task.IsCompleted || taskCompletionSource.Task.IsFaulted))
                {
                    taskCompletionSource.SetResult(new AuthenticationResult
                    {
                        Authenticated = success,
                        ErrorMessage = errorMessage
                    });
                }
            }
        }
    }
}
