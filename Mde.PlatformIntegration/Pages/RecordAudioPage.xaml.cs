using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class RecordAudioPage : ContentPage
{
    private readonly RecordAudioViewModel recordAudioViewModel;

    public RecordAudioPage(RecordAudioViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
        this.recordAudioViewModel = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        recordAudioViewModel.AppearingCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        recordAudioViewModel.DisappearingCommand.Execute(null);
    }

    private void RecordButtonPressed(object sender, EventArgs e)
    {
        recordingAnimation.Progress = TimeSpan.Zero;
        recordingTimeLabel.FadeTo(1, 100);
        recordAudioViewModel.RecordCommand.Execute(null);
    }

    private void RecordButtonReleased(object sender, EventArgs e)
    {
        recordingAnimation.Progress = TimeSpan.Zero;
        recordingTimeLabel.FadeTo(0, 100);
        recordAudioViewModel.StopRecordingCommand.Execute(null);
    }
}