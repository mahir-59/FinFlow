using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;

namespace FinFlow.App
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var color = Android.Graphics.Color.ParseColor("#fcfaf8");
            Window?.SetStatusBarColor(color);
            Window?.SetNavigationBarColor(color);

            var controller = WindowCompat.GetInsetsController(Window, Window.DecorView);

            if (controller != null)
            {
                controller.AppearanceLightStatusBars = true;
                controller.AppearanceLightNavigationBars = true;
            }
        }
    }
}
