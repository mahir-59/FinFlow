using FinFlow.Modules.Auth.Auth.BL.Classes;
using FinFlow.Modules.Auth.Auth.BL.Interfaces;
using FinFlow.Modules.Base.Base.BL;
using FinFlow.Modules.Base.Base.Model;
using FinFlow.WebApp;
using FinFlow.WebApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

builder.Services.AddTransient<ILoginViewModel, LoginViewModel>();
builder.Services.AddScoped<APIRequestHandler>();
builder.Services.AddSingleton<TokenStore>();
builder.Services.AddSingleton<PopupService>();
builder.Services.AddSingleton<LoaderService>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
