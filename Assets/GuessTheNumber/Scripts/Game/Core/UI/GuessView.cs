using TMPro;
using UnityEngine;

namespace GuessTheNumber.Scripts.Game.Core.UI
{
    public class GuessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        
        public void SetGuess(Guess guess)
        {
            string value = GetSign(guess.Result) + guess.Value;
            _value.text = value;
        }

        private string GetSign(GuessResult guessResult)
        {
            switch (guessResult)
            {
                case GuessResult.Bigger:
                    return "меньше ";
                case GuessResult.Smaller:
                    return "больше ";
                case GuessResult.Equal:
                    return "равно ";
            }

            return "";
        }
    }
}