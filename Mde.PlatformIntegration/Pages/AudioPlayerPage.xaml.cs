using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class AudioPlayerPage : ContentPage
{
    private readonly AudioPlayerViewModel audioPlayerViewModel;

    public AudioPlayerPage(AudioPlayerViewModel audioPlayerViewModel)
	{
		InitializeComponent();
        BindingContext = this.audioPlayerViewModel = audioPlayerViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        audioPlayerViewModel.AppearingCommand.Execute(null);
    }
}