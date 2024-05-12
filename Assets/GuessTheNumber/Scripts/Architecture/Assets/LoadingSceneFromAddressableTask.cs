using System.Threading.Tasks;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Patterns.DI;
using UnityEngine.AddressableAssets;

namespace GuessTheNumber.Scripts.Architecture.Assets
{
    public class LoadingSceneFromAddressableTask : TaskObject
    {
        [Inject] private AssetsManager _assetsManager;
        private string _sceneName;
        
        public LoadingSceneFromAddressableTask(string sceneName)
        {
            _sceneName = sceneName;
        }

        protected override void StartInternal()
        {
            _assetsManager.LoadSceneFromAddressable(_sceneName, HandleLoadingProgress, HandleLoadingComplete);
        }
        
        private void HandleLoadingProgress(float progress)
        {
            Progress = progress;
        }

        private void HandleLoadingComplete()
        {
            InvokeComplete();
        }
    }
}