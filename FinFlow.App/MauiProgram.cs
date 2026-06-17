using FinFlow.App.Components.Pages.Auth;
using FinFlow.App.Services;
using FinFlow.Modules.Auth.Auth.BL.Classes;
using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Base.Base.BL;
using FinFlow.Modules.Base.Base.Model;
using FinFlow.Modules.Items.Items.BL.Classes;
using FinFlow.Modules.Items.Items.BL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FinFlow.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // NOTE: These services must be registered for ALL build configurations.
            // Previously they were inside #if DEBUG, so they were missing in Release
            // builds (used when deploying to a physical device). Components such as
            // Dialog/Loader inject DialogService/LoaderService, and the missing
            // registrations caused a silent exception on first render -> blank screen.
            builder.Services.AddTransient<NavigationService>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<TokenStore>();
            builder.Services.AddTransient<APIRequestHandler>();
            builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
            builder.Services.AddTransient<IItemViewModel, ItemViewModel>();
            builder.Services.AddSingleton<LoaderService>();
            builder.Services.AddSingleton<DialogService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
