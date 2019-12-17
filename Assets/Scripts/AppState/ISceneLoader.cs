using System;
using DefaultNamespace;
using UnityEngine;

namespace Manager
{
    public interface ISceneLoader
    {
        AsyncOperation LoadScene(StageAppType stageAppType,
            Action<float> onNext = null, Action callBack = null);

        AsyncOperation LoadSceneViaPreloader(StageAppType stageAppType,
            Action<float> onNext = null, Action callBack = null);

        bool IsLoading { get; }

        AsyncOperation LoadScene(string getAllScenePath, Action<float> onNext = null, Action callBack = null);
    }
}