using MudBlazor;

namespace Postman2CSharp.UI.Models
{
    public class ProgressPopupOptions
    {
        public string Text { get; set; }
        public string CompletedText { get; set; }
        public event Func<string, Task>? StageChanged;
        public event Func<float, Task>? OnProgress;
        public Snackbar? Snackbar { get; set; }

        public ProgressPopupOptions(string text, string completedText)
        {
            Text = text;
            CompletedText = completedText;
        }

        public async Task InvokeProgressCallback(float progress)
        {
            if (OnProgress != null)
            {
                await RaiseProgressCallback(progress);
            }
        }

        private async Task RaiseProgressCallback(float progress)
        {
            var handlers = OnProgress;
            if (handlers != null)
            {
                var tasks = handlers.GetInvocationList()
                    .Cast<Func<float, Task>>()
                    .Select(handler => handler(progress))
                    .ToArray();

                await Task.WhenAll(tasks);
            }
        }

        public async Task InvokeStageChangedCallback(string stage)
        {
            if (StageChanged != null)
            {
                await RaiseStageChangedCallback(stage);
            }
        }

        private async Task RaiseStageChangedCallback(string stage)
        {
            var handlers = StageChanged;
            if (handlers != null)
            {
                var tasks = handlers.GetInvocationList()
                    .Cast<Func<string, Task>>()
                    .Select(handler => handler(stage))
                    .ToArray();

                await Task.WhenAll(tasks);
            }
        }
    }
}
