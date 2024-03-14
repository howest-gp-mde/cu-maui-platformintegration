using Mde.PlatformIntegration.ViewModels;
using System.Diagnostics;

namespace Mde.PlatformIntegration.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();

        BindingContext = mainViewModel;
    }
}