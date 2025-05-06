using DevTools.TraducoesMAUI.Windows;

namespace DevTools.TraducoesMAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new TrdzListWindow())
        {
            Title = "DevTools - Traduções",
            Width = 900,
            MinimumWidth = 900,
            MaximumWidth = 900,
            Height = 600,
            MinimumHeight = 600,
            MaximumHeight = 600,
        };

        // Get display size
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

        // Center the window
        window.X = ( displayInfo.Width / displayInfo.Density - window.Width ) / 2;
        window.Y = ( displayInfo.Height / displayInfo.Density - window.Height ) / 2;

        return window;
    }
}