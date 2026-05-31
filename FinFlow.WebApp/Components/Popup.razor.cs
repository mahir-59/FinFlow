using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using static FinFlow.WebApp.Services.PopupService;

namespace FinFlow.WebApp.Components
{
    public partial class Popup : ComponentBase
    {
        private bool IsVisible;
        private string Message = "";
        private string CssClass = "";

        protected override void OnInitialized()
        {
            PopupService.OnShow += ShowPopup;
        }

        private void Close()
        {
            IsVisible = false;
        }

        private async void ShowPopup(PopupMessage popup)
        {
            Message = popup.Message;

            CssClass = popup.Type switch
            {
                PopupType.Success => "success",
                PopupType.Error => "error",
                PopupType.Warning => "warning",
                _ => "info"
            };

            IsVisible = true;
            StateHasChanged();

            await Task.Delay(3000);

            IsVisible = false;
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            PopupService.OnShow -= ShowPopup;
        }
    }
}
