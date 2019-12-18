using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Handlers
{
   public class TaskMessagesHandler:ITaskMessagesHandler
   {
       public event Action<int> AddMessage;
       public event Action CompleteTask;
       private readonly AsyncProcessor _asyncProcessor;
       private readonly IExperienceHandler _experienceHandler;
       private readonly IGameInfoHolder _gameInfoHolder;
       private Coroutine[] _nextMessageWaitors;
       private Coroutine _timer;
       private Queue<int> _takenTasksTimes;
        private int _currentTaskTimeLeftt;

        public TaskMessagesHandler(AsyncProcessor asyncProcessor, IExperienceHandler experienceHandler, IGameInfoHolder gameInfoHolder)
        {
            _asyncProcessor = asyncProcessor;
            _experienceHandler = experienceHandler;
            _gameInfoHolder = gameInfoHolder;

            _nextMessageWaitors=new Coroutine[3];
            _takenTasksTimes=new Queue<int>();

            StartWaitForNextMessage(0);
            StartWaitForNextMessage(1);
            StartWaitForNextMessage(2);

        }

        public bool CheckTaskTakePossibility()
        {
            return _takenTasksTimes.Count < 15;
          
        }

        public void TakeTask(int teacherNum)
        {
            _takenTasksTimes.Enqueue(_gameInfoHolder.TaskTimes[Random.Range(0, _gameInfoHolder.TaskTimes.Length)]);
            if (_timer ==null)
            
            {
                _timer=_asyncProcessor.StartCoroutine(Timer());
            }
            StartWaitForNextMessage(teacherNum);
        }

        private void StartWaitForNextMessage(int teacherNum)
        {
            if (_nextMessageWaitors[teacherNum]!=null)
            {
                _asyncProcessor.StopCoroutine(_nextMessageWaitors[teacherNum]);
                _nextMessageWaitors[teacherNum] = null;
            }

            int time = 5;
            _nextMessageWaitors[teacherNum] =_asyncProcessor.StartCoroutine(WaitForNextMessage(time,teacherNum));
        }

        public IEnumerator WaitForNextMessage(float time, int teacherNum)
        {
            yield return new WaitForSeconds(time);
            AddMessage?.Invoke(teacherNum);
        }

        public void ReduceTaskTimer()
        {
            _currentTaskTimeLeftt = _currentTaskTimeLeftt-2;
            Debug.Log(_currentTaskTimeLeftt);
        }

        public IEnumerator Timer()
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
                CompleteTask?.Invoke();
            }
            _asyncProcessor.StopCoroutine(_timer);
            _timer = null;

        }

    }
}
