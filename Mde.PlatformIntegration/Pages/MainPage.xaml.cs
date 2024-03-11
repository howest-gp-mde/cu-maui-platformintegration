using Mde.PlatformIntegration.ViewModels;
using System.Diagnostics;

namespace Mde.PlatformIntegration.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel mainViewModel;

    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();

        BindingContext = this.mainViewModel = mainViewModel;
    }
}