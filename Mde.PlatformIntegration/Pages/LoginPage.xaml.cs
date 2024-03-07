using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel loginViewModel;

    public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();

        BindingContext = this.loginViewModel = loginViewModel;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        loginViewModel.OnAppearingCommand.Execute(null);
    }
}