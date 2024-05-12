using System.Collections.Generic;
using UnityEngine;

namespace GuessTheNumber.Scripts.Architecture.UI
{
    public class UIHierarchy : MonoBehaviour
    {
        [SerializeField] private UIWindow[] preloadedWindows;
        [SerializeField] private Transform windowsContainer;

        internal Transform WindowsContainer => windowsContainer;
        internal IReadOnlyList<UIWindow> PreloadedWindows => preloadedWindows;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}