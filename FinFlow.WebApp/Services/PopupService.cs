namespace FinFlow.WebApp.Services
{
    public class PopupService
    {
        public event Action<PopupMessage>? OnShow;


        public event Action<ConfirmationRequest>? OnConfirmRequested;

        public Task<bool> ConfirmAsync(string title, string message)
        {
            var tcs = new TaskCompletionSource<bool>();

            OnConfirmRequested?.Invoke(new ConfirmationRequest
            {
                Title = title,
                Message = message,
                CompletionSource = tcs
            });

            return tcs.Task;
        }

        public void ShowSuccess(string message)
        {
            OnShow?.Invoke(new PopupMessage
            {
                Message = message,
                Type = PopupType.Success
            });
        }

        public void ShowError(string message)
        {
            OnShow?.Invoke(new PopupMessage
            {
                Message = message,
                Type = PopupType.Error
            });
        }

        public void ShowWarning(string message)
        {
            OnShow?.Invoke(new PopupMessage
            {
                Message = message,
                Type = PopupType.Warning
            });
        }

        public void ShowInfo(string message)
        {
            OnShow?.Invoke(new PopupMessage
            {
                Message = message,
                Type = PopupType.Info
            });
        }

        public class PopupMessage
        {
            public string Message { get; set; } = string.Empty;
            public PopupType Type { get; set; }
        }

        public class ConfirmationRequest
        {
            public string Title { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;

            public TaskCompletionSource<bool> CompletionSource { get; set; } = default!;
        }

        public enum PopupType
        {
            Success,
            Error,
            Warning,
            Info
        }
    }
}
