using System.Threading;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace AAA.Utility.Extensions
{
    public static class ButtonExtensions
    {
        public static Task AwaitClick(this Button button, CancellationToken cancellationToken)
        {
            TaskCompletionSource<object> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void Complete()
            {
                button.onClick.RemoveListener(Complete);
                taskCompletionSource.TrySetResult(default);
            }

            button.onClick.AddListener(Complete);
            return taskCompletionSource.Task;
        }
    }
}
