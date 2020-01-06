using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    public class PersonalLifeHandler : IPersonalLifeHandler, IDisposable
    {

       private readonly IGameInfoHolder _gameInfoHolder;
        private readonly IPlayerInfoHolder _playerInfoHolder;
        private readonly AsyncProcessor _asyncProcessor;

        public int MaxPrivateLife => _playerInfoHolder.MaxPrivateLife;
        public int PrivateLife => _playerInfoHolder.PrivateLife;


        private Coroutine _increasePrivateLife;

        public PersonalLifeHandler(IPlayerInfoHolder playerInfoHolder, IGameInfoHolder gameInfoHolder,
            AsyncProcessor asyncProcessor)
        {
            _playerInfoHolder = playerInfoHolder;
            _gameInfoHolder = gameInfoHolder;
            _asyncProcessor = asyncProcessor;
            _increasePrivateLife = _asyncProcessor.StartCoroutine(IncreasePrivateLife());

        }

        public void ReducePrivateLife()
        {
            ChangePrivateLife(-_gameInfoHolder.PrivateLifeClickReduce);
        }

        private void ChangePrivateLife(int value)
        {
            _playerInfoHolder.PrivateLife = _playerInfoHolder.PrivateLife + value;
            if (_playerInfoHolder.PrivateLife > _playerInfoHolder.MaxPrivateLife)
                _playerInfoHolder.PrivateLife = _playerInfoHolder.MaxPrivateLife;
            if (_playerInfoHolder.PrivateLife < 0)
                _playerInfoHolder.PrivateLife = 0;
           }

        private IEnumerator IncreasePrivateLife()
        {
            while (true)
            {
                yield return new WaitForSeconds(_gameInfoHolder.PersonalLifeIncreaseTime);
                if (_playerInfoHolder.PrivateLife < _playerInfoHolder.MaxPrivateLife)
                {
                    _playerInfoHolder.PrivateLife = _playerInfoHolder.PrivateLife + 1;
                }
            }
        }

        public void Dispose()
        {
            _asyncProcessor.StopCoroutine(_increasePrivateLife);
            _increasePrivateLife = null;
        }
    }
}
