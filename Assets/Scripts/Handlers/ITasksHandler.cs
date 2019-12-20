using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Handlers
{
    interface ITasksHandler
    {
        event Action CompleteTask;
        bool CheckTaskTakePossibility();
        void TakeTask(int teacherNum);
        void ReduceTaskTimer();
    }
}
