using System;
using System.Collections.Generic;
using GuessTheNumber.Scripts.Game.Core.UI;
using GuessTheNumber.Scripts.Game.Keyboard;
using GuessTheNumber.Scripts.Patterns.DI;

namespace GuessTheNumber.Scripts.Game.Core
{
    public class GameHudController : SharedObject
    {
        [Inject] private LevelsManager _levelsManager;
        [Inject] private KeyboardController _keyboardController;
        [Inject] private GuessCoreManager _guessCoreManager;
        
        public event Action OnStartClick;
        public event Action<string> OnGuessClick;

        private GameHudView _gameHudView;
        
        public override void Init()
        {
            _levelsManager.OnGameLoaded += HandleGameLoaded;
            _keyboardController.OnValueChanged += HandleKeyboardValueChanged;
            _guessCoreManager.OnWin += HandleWin;
        }

        private void HandleWin(Player player)
        {
            _gameHudView.SetActiveGuessButton(false);

            if (player is HumanPlayer)
            {
                _gameHudView.WinMessage.SetActive(true);
            }
            else
            {
                _gameHudView.LoseMessage.SetActive(true);
            }
        }

        private void HandleGameLoaded()
        {
            _gameHudView = _levelsManager.LevelHierarchy.GameHudView;
            _gameHudView.OnStartClick += OnStartClick;
            _gameHudView.OnGuessClick += HandleGuessClick;

            _levelsManager.OnGameLoaded -= HandleGameLoaded;
        }

        private void HandleGuessClick()
        {
            if (string.IsNullOrEmpty(_keyboardController.CurrentValue))
                return;
            
            OnGuessClick?.Invoke(_keyboardController.CurrentValue);
        }

        private void HandleKeyboardValueChanged(string value)
        {
            _gameHudView.SetCurrentGuessText(value);
        }

        public void SetGuessesHistory(Player player, List<Guess> history)
        {
            if (player is HumanPlayer)
                _gameHudView.SetPlayerGuessesHistory(history);
            else
                _gameHudView.SetEnemyGuessesHistory(history);
        }

        public void ResetView()
        {
            _gameHudView.ResetView();
        }
    }
}