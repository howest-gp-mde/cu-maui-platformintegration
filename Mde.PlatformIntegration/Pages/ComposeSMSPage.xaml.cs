using Mde.PlatformIntegration.ViewModels;

namespace Mde.PlatformIntegration.Pages;

public partial class ComposeSmsPage : ContentPage
{
	public ComposeSmsPage(ComposeSmsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}