using System.Threading;
using System.Threading.Tasks;

namespace AAA.Extensions
{
    public static class TaskCompletionSourceExtensions
    {
        public static void LinkCancellationToken<T>(this TaskCompletionSource<T> taskCompletionSource, CancellationToken ct)
        {
            if (!ct.CanBeCanceled)
            {
                return;
            }

            ct.Register(() => taskCompletionSource.TrySetCanceled());
        }
    }
}
