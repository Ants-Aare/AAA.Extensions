using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Video;

namespace AAA.Extensions
{
    public static class VideoPlayerExtensions
    {
        public static async Task<bool> PrepareAsync(this VideoPlayer videoPlayer, CancellationToken cancellationToken)
        {
            if (videoPlayer.isPrepared)
            {
                return true;
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();

            cancellationToken.Register(() => taskCompletionSource.TrySetResult(false));

            void OnPrepareCompleted(VideoPlayer _)
            {
                taskCompletionSource.SetResult(false);
            }

            void OnPrepareFailed(VideoPlayer _, string error)
            {
                UnityEngine.Debug.LogError($"There was an error preparing video player {error}");

                taskCompletionSource.SetResult(true);
            }

            videoPlayer.prepareCompleted += OnPrepareCompleted;
            videoPlayer.errorReceived += OnPrepareFailed;

            videoPlayer.Prepare();

            var result = await taskCompletionSource.Task;

            videoPlayer.prepareCompleted -= OnPrepareCompleted;
            videoPlayer.errorReceived -= OnPrepareFailed;

            return result;
        }
    }
}
