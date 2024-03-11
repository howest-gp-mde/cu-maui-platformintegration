using CommunityToolkit.Mvvm.Messaging;
using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class AudioPlayerPage : ContentPage
{
    private readonly AudioPlayerViewModel audioPlayerViewModel;
    private readonly IMessenger messenger;

    public AudioPlayerPage(AudioPlayerViewModel audioPlayerViewModel, IMessenger messenger)
	{
		InitializeComponent();
        BindingContext = this.audioPlayerViewModel = audioPlayerViewModel;
        this.messenger = messenger;

        messenger.Register<AudioIsPlayingMessage>(this, async (r, m) =>
        {
            await controlPanel.TranslateTo(0, 0, 250, Easing.SpringOut);
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        audioPlayerViewModel.AppearingCommand.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        audioPlayerViewModel.DisappearingCommand.Execute(null);
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        controlPanel.TranslationY = 210;
    }
}