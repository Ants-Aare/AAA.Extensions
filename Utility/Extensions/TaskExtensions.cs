using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class TaskExtensions
    {
        public static YieldAwaitable AwaitNextFrame()
        {
            return Task.Yield();
        }

        public static async Task AwaitUntil(Func<bool> func)
        {
            while (!func.Invoke())
            {
                await Task.Yield();
            }
        }

        public static async Task AwaitUntil(Func<bool> func, CancellationToken cancellationToken)
        {
            while (!func.Invoke())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                await Task.Yield();
            }
        }

        public static async void RunAsync(this Task task)
        {
            await task;
        }

        public static async void RunAsync(this Task task, Action<Exception> onException)
        {
            try
            {
                await task;
            }
            catch (Exception exception)
            {
                onException?.Invoke(exception);
            }
        }

        public static async void RunAsync(this Task task, Action onComplete)
        {
            await task;

            onComplete.Invoke();
        }

        public static async void RunAsync<T>(this Task<T> task, Action<T> onComplete)
        {
            T result = await task;

            onComplete.Invoke(result);
        }

        public static IEnumerator ToCoroutine(this Task task)
        {
            task.RunAsync();

            yield return new WaitUntil(() => task.IsCompleted);
        }

        public static IEnumerator ToCoroutine(this Task task, Action<Exception> onException)
        {
            task.RunAsync(onException);

            yield return new WaitUntil(() => task.IsCompleted);
        }
    }
}
