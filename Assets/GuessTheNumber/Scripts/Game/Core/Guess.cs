namespace GuessTheNumber.Scripts.Game.Core
{
    public readonly struct Guess
    {
        public int Value { get; }
        public GuessResult Result { get; }
        
        public Guess(int value, GuessResult result)
        {
            Value = value;
            Result = result;
        }
    }
}