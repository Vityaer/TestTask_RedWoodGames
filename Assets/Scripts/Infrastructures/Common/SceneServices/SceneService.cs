using Cysharp.Threading.Tasks;
using Infrastructures.Common.SceneServices;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Asyncs;

namespace Infrastructures.Common
{
    public class SceneService : ISceneService, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSourceLoadingScene;

        public void LoadScene(string name)
        {
            _cancellationTokenSourceLoadingScene.TryCancel();
            _cancellationTokenSourceLoadingScene = new();
            LoadScene(name, LoadSceneMode.Single, _cancellationTokenSourceLoadingScene.Token).Forget();
        }

        private async UniTaskVoid LoadScene(string sceneName, LoadSceneMode mode, CancellationToken cancellationToken)
        {
            var currentScene = SceneManager.GetActiveScene();

            var asyncOp = SceneManager.LoadSceneAsync(sceneName, mode);
            if (asyncOp == null)
                return;

            await asyncOp.ToUniTask(cancellationToken: cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                return;

            var targetScene = SceneManager.GetSceneByName(sceneName);

            SceneManager.SetActiveScene(targetScene);

            if (mode == LoadSceneMode.Additive)
            {
                await SceneManager.UnloadSceneAsync(currentScene.name).ToUniTask(cancellationToken: cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    return;
            }

            await Resources.UnloadUnusedAssets().ToUniTask(cancellationToken: cancellationToken);
        }

        public void Dispose()
        {
            _cancellationTokenSourceLoadingScene.TryCancel();
        }
    }
}
