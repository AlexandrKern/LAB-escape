using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader
{
    public async UniTask LoadNextScene(CancellationToken cancellationToken, string SceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }

            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }
    }

    public async void LoadSceneAsync(string sceneName)
    {
        await LoadNextScene(CancellationToken.None, sceneName);
    }

    // ѕерегрузка метода с ожиданием конца видео
    public async void LoadSceneAsync(VideoPlayer videoPlayer, string sceneName)
    {
        var videoEndTask = WaitForVideoEnd(videoPlayer);
        var keyPressTask = UniTask.WaitUntil(() => Input.anyKeyDown);
        await UniTask.WhenAny(videoEndTask, keyPressTask);
        await LoadNextScene(CancellationToken.None, sceneName);
    }


    // ћетод ожидани€ завершени€ воспроизведени€ видео
    private async UniTask WaitForVideoEnd(VideoPlayer videoPlayer)
    {
        var videoEndCompletionSource = new UniTaskCompletionSource();
        videoPlayer.loopPointReached += OnVideoEnd;
        await videoEndCompletionSource.Task;

        void OnVideoEnd(VideoPlayer vp)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
            videoEndCompletionSource.TrySetResult();
        }
    }
}
