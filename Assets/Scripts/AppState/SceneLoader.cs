using System;

using DataProvider;
using DefaultNamespace;
using Game;
using Manager;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IAppStateHandler _appStateHandler;
        private readonly ILocalDataProvider _localDataProvider;

        public SceneLoader(IAppStateHandler appStateHandler, ILocalDataProvider localDataProvider)
        {
            _appStateHandler = appStateHandler;
            _localDataProvider = localDataProvider;
        }

        public bool IsLoading { get; private set; }

        /// <summary>
        /// Load target scene asynchronously through preloader scene
        /// </summary>
        public AsyncOperation LoadSceneViaPreloader(StageAppType stageAppType, Action<float> onNext = null,
            Action callBack = null)
        {
            _appStateHandler.SetData(StageAppType.Preloader, stageAppType);
            _localDataProvider.Save(stageAppType);
            return LoadScene(StageAppType.Preloader, onNext, callBack);
        }

        /// <summary>
        /// Load target scene asynchronously
        /// </summary>
        public AsyncOperation LoadScene(StageAppType stageAppType,
            Action<float> onNext = null,
            Action callBack = null)
        {
            IsLoading = true;

            return LoadSceneObservable(stageAppType.ToString(), s => { onNext?.Invoke(s); }, () =>
            {
                _appStateHandler.ChangeState(stageAppType);
                IsLoading = false;
                callBack?.Invoke();
            });
        }

        public AsyncOperation LoadScene(string getAllScenePath,
            Action<float> onNext = null,
            Action callBack = null)
        {
            IsLoading = true;

            return LoadSceneObservable(getAllScenePath, onNext, () =>
            {
                _appStateHandler.ChangeState(StageAppType.None);
                IsLoading = false;
                callBack?.Invoke();
            });
        }

        private static AsyncOperation LoadSceneObservable(string getAllScenePath, Action<float> progress, Action callBack)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(getAllScenePath);
                
            asyncOperation.allowSceneActivation = false;
            asyncOperation.ConfigureAwait(Progress.Create<float>(x =>
            {
                if (Mathf.Approximately(asyncOperation.progress, 0.9f))
                {
                    asyncOperation.allowSceneActivation = true;
                }
                progress?.Invoke(x);
            }));
            asyncOperation.completed += operation =>
            {
                asyncOperation.allowSceneActivation = true;
                callBack?.Invoke();
            };

            return asyncOperation;
        }
    }
}