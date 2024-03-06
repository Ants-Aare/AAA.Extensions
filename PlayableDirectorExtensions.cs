using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Playables;

namespace AAA.Extensions
{
    public static class PlayableDirectorExtensions
    {
        public static Task Play(this PlayableDirector playableDirector, CancellationToken cancellationToken)
        {
            playableDirector.Play();

            return AwaitCompletition(playableDirector, cancellationToken);
        }

        public static Task Play(this PlayableDirector playableDirector, PlayableAsset playableAsset, CancellationToken cancellationToken)
        {
            playableDirector.Play(playableAsset);

            return AwaitCompletition(playableDirector, cancellationToken);
        }

        public static Task AwaitCompletition(this PlayableDirector playableDirector, CancellationToken cancellationToken)
        {
            if (playableDirector.state == PlayState.Playing)
            {
                return Task.CompletedTask;
            }

            TaskCompletionSource<object> taskCompletionSource = new();
            taskCompletionSource.LinkCancellationToken(cancellationToken);

            void OnStoped(PlayableDirector _) => taskCompletionSource.TrySetResult(default);

            playableDirector.stopped += OnStoped;

            cancellationToken.Register(() => OnStoped(playableDirector));

            return taskCompletionSource.Task;
        }

        public static void Complete(this PlayableDirector playableDirector)
        {
            if (playableDirector.state != PlayState.Playing)
            {
                return;
            }

            for (var i = 0; i < playableDirector.playableGraph.GetRootPlayableCount(); ++i)
            {
                playableDirector.playableGraph.GetRootPlayable(i).SetSpeed(double.MaxValue);
            }
        }
    }
}
