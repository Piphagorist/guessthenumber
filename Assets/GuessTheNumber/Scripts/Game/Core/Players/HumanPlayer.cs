using GuessTheNumber.Scripts.Game.Keyboard;
using GuessTheNumber.Scripts.Patterns.DI;

namespace GuessTheNumber.Scripts.Game.Core
{
    public class HumanPlayer : Player
    {
        [Inject] private KeyboardController _keyboardController;
        [Inject] private GameHudController _gameHudController;

        public void Init()
        {
            _gameHudController.OnGuessClick += HandleGuessClick;
        }

        private void HandleGuessClick(string inputValue)
        {
            int number = int.Parse(inputValue);
            TryToGuess(number);
            _keyboardController.Mix();
        }

        public override void Reset()
        {
            base.Reset();

            _keyboardController.Mix();
            _keyboardController.ResetValue();
            SetLock(false);
        }

        public override void SetLock(bool locked)
        {
            _keyboardController.SetLock(locked);
        }
    }
}