using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Architecture.UI;

namespace GuessTheNumber.Scripts.Architecture.Loading.UI
{
    public class LoadingScreenController : UIWindowController<LoadingScreen>
    {
        public void SetTasksQueue(TasksQueue tasksQueue)
        {
            tasksQueue.OnUpdate += HandleQueueUpdate;
        }

        private void HandleQueueUpdate(ITask obj)
        {
            _window.SetProgress(obj.Progress);
        }
    }
}