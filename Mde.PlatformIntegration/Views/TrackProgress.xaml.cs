using System.Diagnostics;

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
        var progressBar = (Border)trackProgress.FindByName("progressBar");

        AbsoluteLayout.SetLayoutBounds(progressBar, new Rect(0, 0, progress, 1));
    }

    private double lastTotalX;

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        float speedFactor = 1.15f; //slider moves faster than finger, for a more responsive feel
       
        switch (e.StatusType)
        {
            case GestureStatus.Started:
            case GestureStatus.Running:

                var deltaX = e.TotalX - lastTotalX;

                double newX = progressBar.Width + (deltaX * speedFactor);
                newX = Math.Clamp(newX, 0, this.Width);
                double relativeX = newX / this.Width;

                AbsoluteLayout.SetLayoutBounds(progressBar, new Rect(0, 0, relativeX, 1));
                Progress = relativeX;

                lastTotalX = e.TotalX;

                break;
            case GestureStatus.Completed:
                lastTotalX = 0;
                break;
            case GestureStatus.Canceled:
                break;
        }
    }

    private void PointerGestureRecognizer_PointerPressed(object sender, PointerEventArgs e)
    {
        if (sender is View tappedView)
        {
            Debug.WriteLine($"Pointer Pressed: {e.GetPosition(tappedView)}");
        }   
    }

    private void PointerGestureRecognizer_PointerMoved(object sender, PointerEventArgs e)
    {
        if (sender is View tappedView)
        {
            Debug.WriteLine($"Pointer Moved: {e.GetPosition(tappedView)}");
        }
    }

    private void PointerGestureRecognizer_PointerReleased(object sender, PointerEventArgs e)
    {

        if (sender is View tappedView)
        {
            Debug.WriteLine($"Pointer Released: {e.GetPosition(tappedView)}");
        }
    }
}