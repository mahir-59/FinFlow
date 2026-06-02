using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace FinFlow.App.Components.Pages.Auth;

public partial class VerifyOtp
{
    [Inject] private IJSRuntime JS { get; set; } = default!;

    private ElementReference _otpInput;

    private string OtpValue = string.Empty;

    private int _secondsRemaining = 30;
    private System.Threading.Timer? _timer;

    private string TimeRemaining =>
        TimeSpan.FromSeconds(_secondsRemaining).ToString(@"mm\:ss");

    private bool CanResend => _secondsRemaining <= 0;

    private async Task HandleOtpInput(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? "";

        value = new string(value.Where(char.IsDigit).ToArray());

        if (value.Length > 6)
            value = value[..6];

        OtpValue = value;

        StateHasChanged();

        if (OtpValue.Length == 6)
        {
            // Verify OTP API call here
        }
    }

    private async Task FocusInput()
    {
        await JS.InvokeVoidAsync("focusElement", _otpInput);
    }

    protected override void OnInitialized()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _secondsRemaining = 30;

        _timer?.Dispose();

        _timer = new System.Threading.Timer(_ =>
        {
            if (_secondsRemaining > 0)
            {
                _secondsRemaining--;

                InvokeAsync(StateHasChanged);
            }
        },
        null,
        TimeSpan.Zero,
        TimeSpan.FromSeconds(1));
    }

    private async Task ResendOtp()
    {
        // Call Resend OTP API here

        StartTimer();
    }

    private async Task Verifyotp()
    {
        if (OtpValue.Length == 6)
        {
            // Verify OTP API call here
        }
        _navigationManager.NavigateTo("/create-password");
    }

    public async Task GoBack()
    {
        await NavigationService.RaiseBackPressed();
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}