namespace GuessTheNumber.Scripts.Game.Core
{
    public class AiPlayer : Player
    {
        private int _minValue;
        private int _maxValue;
        
        public override void SetLock(bool locked)
        {
            if (locked) return;
            
            int number = GuessNumber();
            
            TryToGuess(number);

            Guess lastGuess = _guesses[^1];

            if (lastGuess.Result == GuessResult.Bigger)
                _maxValue = lastGuess.Value;
            else if (lastGuess.Result == GuessResult.Smaller)
                _minValue = lastGuess.Value;
        }

        public override void Reset()
        {
            base.Reset();

            _minValue = _guessCoreManager.Config.MinValue;
            _maxValue = _guessCoreManager.Config.MaxValue;
        }

        private int GuessNumber()
        {
            return (_minValue + _maxValue) / 2;
        }
    }
}