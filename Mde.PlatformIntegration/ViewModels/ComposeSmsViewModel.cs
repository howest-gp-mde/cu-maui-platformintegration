using CommunityToolkit.Mvvm.ComponentModel;

namespace Mde.PlatformIntegration.ViewModels
{
    public class ComposeSmsViewModel : ObservableObject
    {
        private readonly ISms smsService;

        public ComposeSmsViewModel(ISms smsService)
        {
            this.smsService = smsService;
            SendCommand = new Command(OnSendCommand);
        }

        private string recipient;
        public string Recipient
        {
            get { 
                return recipient;
            }
            set
            {
                SetProperty(ref recipient, value);
            }
        }

        private string body;

        public string Body
        {
            get { 
                return body; 
            }
            set
            {
                SetProperty(ref body, value);
            }
        }

        public Command SendCommand { get; }

        private async void OnSendCommand()
        {
            await smsService.ComposeAsync(new SmsMessage(Body, Recipient));
            Recipient = string.Empty;
            Body = string.Empty;
        }

    }
}
