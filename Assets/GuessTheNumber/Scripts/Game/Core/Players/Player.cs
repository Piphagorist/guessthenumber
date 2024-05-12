using System.Collections.Generic;
using GuessTheNumber.Scripts.Patterns.DI;

namespace GuessTheNumber.Scripts.Game.Core
{
    public abstract class Player
    {
        [Inject] protected GuessCoreManager _guessCoreManager;
        [Inject] private GameHudController _gameHudController;
        
        protected List<Guess> _guesses = new();

        public virtual void Reset()
        {
            _guesses = new List<Guess>();
        }

        public abstract void SetLock(bool locked);

        protected void TryToGuess(int number)
        {
            GuessResult result = _guessCoreManager.TryToGuess(number, this);
            _guesses.Add(new Guess(number, result));
            _gameHudController.SetGuessesHistory(this, _guesses);
        }
    }
}