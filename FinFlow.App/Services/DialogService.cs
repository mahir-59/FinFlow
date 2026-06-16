using System;
using System.Threading.Tasks;

namespace FinFlow.App.Services
{
    public class DialogService
    {
        public enum DialogType
        {
            Success,
            Error,
            Warning,
            Info,
            Confirm
        }

        public DialogType Type { get; set; }
        public event Action? OnShow;

        public bool IsVisible { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Message { get; private set; } = string.Empty;

        public bool IsConfirmation { get; private set; }

        private TaskCompletionSource<bool>? _confirmationSource;

        public void ShowError(string message, DialogType type = DialogType.Error)
        {
            Title = "Error";
            Message = message;
            Type = type;

            IsConfirmation = false;
            IsVisible = true;

            OnShow?.Invoke();
        }
        public void ShowSuccess(string message, DialogType type = DialogType.Success)
        {
            Title = "Success";
            Message = message;
            Type = type;

            IsConfirmation = false;
            IsVisible = true;

            OnShow?.Invoke();
        }

        public Task<bool> ShowConfirmation(string title, string message)
        {
            Title = title;
            Message = message;

            Type = DialogType.Confirm;

            IsConfirmation = true;
            IsVisible = true;

            _confirmationSource = new TaskCompletionSource<bool>();

            OnShow?.Invoke();

            return _confirmationSource.Task;
        }

        public void Confirm()
        {
            IsVisible = false;
            _confirmationSource?.SetResult(true);

            OnShow?.Invoke();
        }

        public void Cancel()
        {
            IsVisible = false;
            _confirmationSource?.SetResult(false);

            OnShow?.Invoke();
        }

        public void Close()
        {
            IsVisible = false;

            OnShow?.Invoke();
        }
    }
}