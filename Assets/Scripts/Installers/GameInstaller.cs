using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class GameInstaller: MonoInstaller
    {

        [SerializeField] private UnityPoolObject[] _viewPrefabs;
        [SerializeField] private Transform _poolRoot;


        public override void InstallBindings()
        {
            BindPools();
        }

        private void BindPools()
        {
            Container.BindInstance(_viewPrefabs);
            Container.Bind<UnityPool.Factory>().AsSingle();
            Container.Bind<UnityPool>().AsSingle().WithArguments(_poolRoot);

        }

    }
}
