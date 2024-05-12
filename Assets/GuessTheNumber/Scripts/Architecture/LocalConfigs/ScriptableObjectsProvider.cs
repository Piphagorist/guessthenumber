using System;
using System.Collections.Generic;
using GuessTheNumber.Scripts.Architecture.Assets;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Patterns.DI;
using UnityEngine;

namespace GuessTheNumber.Scripts.Architecture.LocalConfigs
{
    public class ScriptableObjectsProvider : SharedObject, IPreloadable
    {
        private Dictionary<Type, List<ScriptableObject>> _configs = new();

        public ITask Preload()
        {
            var task = new LoadingAddressableAssetsTask<ScriptableObject>("Config");
            task.OnComplete += HandleLoaded;
            return task;
        }

        private void HandleLoaded(ITask task)
        {
            var assetsTask = task as LoadingAddressableAssetsTask<ScriptableObject>;
            foreach (var config in assetsTask.Result)
            {
                var type = config.GetType();
                if (!_configs.ContainsKey(type))
                    _configs.Add(type, new List<ScriptableObject>());
                
                _configs[type].Add(config);
            }
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (!_configs.ContainsKey(type) || _configs[type].Count == 0)
                throw new Exception($"Config {type} not exist!");

            return _configs[type][0] as T;
        }
        
        public List<T> GetConfigs<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (!_configs.ContainsKey(type) || _configs[type].Count == 0)
                throw new Exception($"Config {type} not exist!");

            return _configs[type] as List<T>;
        }
    }
}