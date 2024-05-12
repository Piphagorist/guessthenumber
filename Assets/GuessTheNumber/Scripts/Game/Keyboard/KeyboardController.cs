using System;
using GuessTheNumber.Scripts.Game.Core;
using GuessTheNumber.Scripts.Game.Keyboard.UI;
using GuessTheNumber.Scripts.Patterns.DI;
using Random = UnityEngine.Random;

namespace GuessTheNumber.Scripts.Game.Keyboard
{
    public class KeyboardController : SharedObject
    {
        [Inject] private LevelsManager _levelsManager;
        [Inject] private GameHudController _gameHudController;

        public event Action<string> OnValueChanged; 

        public string CurrentValue { get; private set; }
        
        private KeyboardView _keyboardView;
        private string[] _keyboardKeys = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        
        public override void Init()
        {
            _levelsManager.OnGameLoaded += HandleGameLoaded;
            _gameHudController.OnGuessClick += HandleGuessClick;
        }

        private void HandleGuessClick(string obj)
        {
            ResetValue();
        }

        private void HandleGameLoaded()
        {
            _keyboardView = _levelsManager.LevelHierarchy.KeyboardView;
            _keyboardView.OnKeyClick += HandleKeyClick;
            _keyboardView.OnBackspaceClick += HandleBackspaceClick;
            _levelsManager.OnGameLoaded -= HandleGameLoaded;
        }

        private void HandleBackspaceClick()
        {
            if (string.IsNullOrEmpty(CurrentValue)) return;
            
            CurrentValue = CurrentValue.Substring(0, CurrentValue.Length - 1);
            OnValueChanged?.Invoke(CurrentValue);
        }

        private void HandleKeyClick(string keyValue)
        {
            CurrentValue += keyValue;
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void Mix()
        {
            for (int i = 0; i < _keyboardKeys.Length; i++)
            {
                int randomIndex = Random.Range(0, _keyboardKeys.Length);
                string key1 = _keyboardKeys[i];
                string key2 = _keyboardKeys[randomIndex];
                
                _keyboardKeys[i] = key2;
                _keyboardKeys[randomIndex] = key1;
            }
            
            _keyboardView.SetKeys(_keyboardKeys);
        }

        public void SetLock(bool locked)
        {
            _keyboardView.SetLock(locked);
        }

        public void ResetValue()
        {
            CurrentValue = "";
            OnValueChanged?.Invoke(CurrentValue);
        }
    }
}