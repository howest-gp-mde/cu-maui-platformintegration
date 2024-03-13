using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel profileViewModel;

    public ProfilePage(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
        BindingContext = this.profileViewModel = profileViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        profileViewModel.AppearingCommand.Execute(null);
    }

}