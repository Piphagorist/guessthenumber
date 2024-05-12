using System.Collections.Generic;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Patterns.DI;

namespace GuessTheNumber.Scripts.Architecture.Assets
{
    public class LoadingAddressableAssetsTask<T> : TaskObject
    {
        [Inject] private AssetsManager _assetsManager;
        
        public IList<T> Result { get; private set; }
        
        private string _label;
        
        public LoadingAddressableAssetsTask(string label)
        {
            _label = label;
        }
        
        protected override void StartInternal()
        {
            _assetsManager.LoadAssetsFromAddressable<T>(_label, HandleLoadingProgress, HandleLoadingComplete);
        }
        
        private void HandleLoadingProgress(float progress)
        {
            Progress = progress;
        }

        private void HandleLoadingComplete(IList<T> result)
        {
            Result = result;
            InvokeComplete();
        }
    }
}