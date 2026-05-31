using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinFlow.WebApp.Components
{
    public partial class Loader : ComponentBase, IDisposable
    {
        private bool IsVisible;

        protected override void OnInitialized()
        {
            LoaderService.OnLoaderChanged += ToggleLoader;
        }

        private void ToggleLoader(bool show)
        {
            IsVisible = show;
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            LoaderService.OnLoaderChanged -= ToggleLoader;
        }
    }
}
