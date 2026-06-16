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

#if DEBUG

            builder.Services.AddTransient<NavigationService>();
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddTransient<TokenStore>();
            builder.Services.AddTransient<APIRequestHandler>();
            builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
            builder.Services.AddTransient<IItemViewModel, ItemViewModel>();
            builder.Services.AddSingleton<LoaderService>();
            builder.Services.AddSingleton<DialogService>();
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
