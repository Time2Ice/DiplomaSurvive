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
        private readonly AsyncProcessor _asyncProcessor;
        public event Action<int> AddMessage;
        private Coroutine[] _nextMessageWaitors;
      
        public TaskMessagesHandler(AsyncProcessor asyncProcessor)
        {
            _asyncProcessor = asyncProcessor;
            _nextMessageWaitors=new Coroutine[3];
          
            StartWaitForNextMessage(0);
            StartWaitForNextMessage(1);
            StartWaitForNextMessage(2);

        }

       
        public void StartWaitForNextMessage(int teacherNum)
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

       

    }
}
