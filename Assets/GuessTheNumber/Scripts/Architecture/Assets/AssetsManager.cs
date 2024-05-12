using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Patterns.DI;
using UnityEngine.AddressableAssets;

namespace GuessTheNumber.Scripts.Architecture.Assets
{
    public class AssetsManager : SharedObject
    {
        public async void LoadSceneFromAddressable(string sceneName, Action<float> OnProgress = null,
            Action OnComplete = null)
        {
            var operationHandle = Addressables.LoadSceneAsync(sceneName);

            while (!operationHandle.IsDone)
            {
                await Task.Delay(100);
                OnProgress?.Invoke(operationHandle.PercentComplete);
            }

            OnProgress?.Invoke(1.0f);
            OnComplete?.Invoke();
        }
        
        public async void LoadAssetsFromAddressable<T>(string label, Action<float> OnProgress = null,
            Action<IList<T>> OnComplete = null)
        {
            var operationHandle = Addressables.LoadAssetsAsync<T>(label, null);

            while (!operationHandle.IsDone)
            {
                await Task.Delay(100);
                OnProgress?.Invoke(operationHandle.PercentComplete);
            }

            OnProgress?.Invoke(1.0f);
            OnComplete?.Invoke(operationHandle.Result);
        }
    }
}