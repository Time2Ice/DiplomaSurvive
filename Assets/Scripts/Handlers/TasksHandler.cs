using Assets.Scripts.Data;
using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
   public  class TasksHandler:ITasksHandler
    {
        public event Action CompleteTask;

        private readonly AsyncProcessor _asyncProcessor;
        private readonly IExperienceHandler _experienceHandler;
        private readonly IGameInfoHolder _gameInfoHolder;
        private IPlayerInfoHolder _playerInfoHolder;
        private Coroutine _timer;
        private Queue<int> _takenTasksTimes;
        private int _currentTaskTimeLeftt;

        public TasksHandler(AsyncProcessor asyncProcessor, IPlayerInfoHolder playerInfoHolder, IExperienceHandler experienceHandler, IGameInfoHolder gameInfoHolder)
        {
            _asyncProcessor = asyncProcessor;
            _experienceHandler = experienceHandler;
            _gameInfoHolder = gameInfoHolder;
            _playerInfoHolder = playerInfoHolder;
            _takenTasksTimes = new Queue<int>();
            for(int i=0; i<_playerInfoHolder.TasksTaken;i++)
            {
                _takenTasksTimes.Enqueue(_gameInfoHolder.TaskTimes[UnityEngine.Random.Range(0, _gameInfoHolder.TaskTimes.Length)]);
            }
        }

        public bool CheckTaskTakePossibility()
        {
            return _takenTasksTimes.Count < _playerInfoHolder.TaskQueueCapasity;
        }

        public void TakeTask(int teacherNum)
        {
            _takenTasksTimes.Enqueue(_gameInfoHolder.TaskTimes[UnityEngine.Random.Range(0, _gameInfoHolder.TaskTimes.Length)]);
            _playerInfoHolder.TasksTaken = _playerInfoHolder.TasksTaken + 1;
            if (_timer == null)

            {
                _timer = _asyncProcessor.StartCoroutine(Timer());
            }
        }

        public void ReduceTaskTimer()
        {
            _currentTaskTimeLeftt = _currentTaskTimeLeftt - 2;
        }

        private IEnumerator Timer()
        {
            while (_takenTasksTimes.Count > 0)
            {
                _currentTaskTimeLeftt = _takenTasksTimes.Dequeue();
                while (_currentTaskTimeLeftt > 0)
                {
                    yield return new WaitForSeconds(1);
                    _currentTaskTimeLeftt--;
                }
                _experienceHandler.ChangeExperience();
                _playerInfoHolder.Coins = _playerInfoHolder.Coins+_gameInfoHolder.TaskCompleteCoins;
                _playerInfoHolder.TasksTaken = _playerInfoHolder.TasksTaken - 1;
                CompleteTask?.Invoke();
            }
            _asyncProcessor.StopCoroutine(_timer);
            _timer = null;

        }
    }
}
