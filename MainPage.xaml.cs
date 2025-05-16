using Microsoft.Extensions.Logging;


namespace EmpowerPlant;
public partial class MainPage
{
    private readonly ILogger<MainPage> _logger;

    private int _count = 0;

    // NOTE: You can only inject an ILogger<T>, not a plain ILogger
    public MainPage(ILogger<MainPage> logger)
    {
        _logger = logger;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
#if !ANDROID
        JavaCrashBtn.IsVisible = false;
#endif

#if !__MOBILE__
        NativeCrashBtn.IsVisible = false;
#endif
        base.OnAppearing();
    }

    private void OnUnhandledExceptionClicked(object sender, EventArgs e)
    {
#pragma warning disable CS0618
        SentrySdk.CauseCrash(CrashType.Managed);
#pragma warning restore CS0618
    }

    private void OnBackgroundThreadUnhandledExceptionClicked(object sender, EventArgs e)
    {
#pragma warning disable CS0618
        SentrySdk.CauseCrash(CrashType.ManagedBackgroundThread);
#pragma warning restore CS0618
    }

    private void OnCapturedExceptionClicked(object sender, EventArgs e)
    {
        try
        {
            throw new ApplicationException("This exception was thrown and captured manually, without crashing the app.");
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
        }
    }
    private void OnJavaCrashClicked(object sender, EventArgs e)
    {
#if ANDROID
#pragma warning disable CS0618
        SentrySdk.CauseCrash(CrashType.Java);
#pragma warning restore CS0618
#endif
    }

    private void OnNativeCrashClicked(object sender, EventArgs e)
    {
#if __MOBILE__
#pragma warning disable CS0618
        SentrySdk.CauseCrash(CrashType.Native);
#pragma warning restore CS0618
#endif
    }

    async void BtnRealWorld_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListPage", true);
    }
}