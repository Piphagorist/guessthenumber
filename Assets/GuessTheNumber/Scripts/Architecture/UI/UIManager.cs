using System;
using System.Collections.Generic;
using GuessTheNumber.Scripts.Patterns.DI;
using UnityEngine.AddressableAssets;

namespace GuessTheNumber.Scripts.Architecture.UI
{
    public class UIManager : SharedObject
    {
        private UIHierarchy _hierarchy;
        private Dictionary<Type, UIWindow> _windows = new();

        public void SetHierarchy(UIHierarchy hierarchy)
        {
            _hierarchy = hierarchy;

            foreach (var window in _hierarchy.PreloadedWindows)
                _windows.Add(window.GetType(), window);
        }

        public T GetWindow<T>() where T : UIWindow
        {
            var type = typeof(T);

            if (_windows.TryGetValue(type, out var window))
                return (T)window;
            else
                return Addressables.LoadAssetAsync<T>("Windows").Result;
        }
    }
}