using Microsoft.JSInterop;
public class NavigationService
{
    public static event Func<Task>? BackPressed;

    public static async Task RaiseBackPressed()
    {
        if (BackPressed != null)
            await BackPressed.Invoke();
    }
}
