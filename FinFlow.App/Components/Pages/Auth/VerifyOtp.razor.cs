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
}