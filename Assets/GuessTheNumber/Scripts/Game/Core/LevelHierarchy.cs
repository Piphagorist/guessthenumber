using System;
using GuessTheNumber.Scripts.Game.Core.UI;
using GuessTheNumber.Scripts.Game.Keyboard.UI;
using UnityEngine;

namespace GuessTheNumber.Scripts.Game.Core
{
    public class LevelHierarchy : MonoBehaviour
    {
        [field: SerializeField] public KeyboardView KeyboardView { get; private set; }
        [field: SerializeField] public GameHudView GameHudView { get; private set; }
        
        public static event Action<LevelHierarchy> OnLoad;

        private void Awake()
        {
            OnLoad?.Invoke(this);
            OnLoad = null;
        }
    }
}