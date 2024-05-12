using GuessTheNumber.Scripts.Architecture.Assets;
using GuessTheNumber.Scripts.Architecture.Loading.UI;
using GuessTheNumber.Scripts.Architecture.LocalConfigs;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Architecture.UI;
using GuessTheNumber.Scripts.Game.Core;
using GuessTheNumber.Scripts.Game.Keyboard;
using GuessTheNumber.Scripts.Patterns.DI;
using UnityEngine;

namespace GuessTheNumber.Scripts.Game
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private UIHierarchy uiHierarchy;
        
        private GlobalContainer _container;
        private LoadingScreenController _loadingScreenController;
        
        private void Awake()
        {
            Physics2D.simulationMode = SimulationMode2D.Update;
            
            CreateModules();

            _container.Get<UIManager>().SetHierarchy(uiHierarchy);
            
            var queue = new TasksQueue();
            AddPreloadTasksToQueue(queue);
            queue.AddTask(new TaskAction(_container.InitAll));
            queue.AddTask(new LoadingSceneFromAddressableTask("Game"));
            
            
            queue.OnComplete += HandleLoadingComplete;

            _loadingScreenController = _container.Get<LoadingScreenController>();
            _loadingScreenController.ShowWindow();
            _loadingScreenController.SetTasksQueue(queue);
            
            queue.Start();
        }

        private void CreateModules()
        {
            _container = GlobalContainer.Instance;
            
            _container.Add<ScriptableObjectsProvider>();
            _container.Add<UIManager>();
            _container.Add<LoadingScreenController>();
            _container.Add<AssetsManager>();
            _container.Add<GameHudController>();
            _container.Add<GuessCoreManager>();
            _container.Add<LevelsManager>();
            _container.Add<KeyboardController>();

            _container.InjectAll();
        }

        private void AddPreloadTasksToQueue(TasksQueue queue)
        {
            queue.AddTask(_container.Get<ScriptableObjectsProvider>().Preload());
        }

        private void HandleLoadingComplete(ITask task)
        {
            _loadingScreenController.HideWindow();
        }
    }
}