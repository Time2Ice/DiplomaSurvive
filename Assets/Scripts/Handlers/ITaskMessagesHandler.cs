
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handlers
{
   public interface ITaskMessagesHandler
    { 
        event Action<int> AddMessage;

        void StartWaitForNextMessage(int teacherNum);
      
    }
}
