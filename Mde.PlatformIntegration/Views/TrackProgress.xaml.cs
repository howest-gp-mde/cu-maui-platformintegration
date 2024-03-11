namespace Mde.PlatformIntegration.Views;

public partial class TrackProgress : ContentView
{
	public TrackProgress()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty ProgressProperty =
    BindableProperty.Create(
        propertyName: nameof(ProgressProperty),
        returnType: typeof(double),
        declaringType: typeof(TrackProgress),
        defaultValue: default(double), propertyChanged: OnProgressChanged);

    public double Progress
    {
        get => (double)GetValue(ProgressProperty);
        set
        {
            SetValue(ProgressProperty, value);
        }
    }

    static void OnProgressChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var trackProgress = (TrackProgress)bindable;
        double progress = (double)newValue;
        double oldProgress = (double)oldValue;
        var progressBar = (Border) trackProgress.FindByName("progressBar");
        //await progressBar.LayoutTo(new Rect(0, 0, progress, 1), 16, Easing.Linear);
        if (oldProgress > progress && progress != 0) 
            return;

        AbsoluteLayout.SetLayoutBounds(progressBar, new Rect(0, 0, progress, 1));
    }
}