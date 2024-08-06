using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
