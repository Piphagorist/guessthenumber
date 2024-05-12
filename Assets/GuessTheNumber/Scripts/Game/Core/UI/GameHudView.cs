using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.Scripts.Game.Core.UI
{
    public class GameHudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentGuessText;
        [SerializeField] private Transform _playerGuessesContainer;
        [SerializeField] private Transform _enemyGuessesContainer;
        [SerializeField] private GuessView _guessViewPrefab;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _guessButton;
        [field: SerializeField] public GameObject WinMessage { get; private set; }
        [field: SerializeField] public GameObject LoseMessage { get; private set; }

        public event Action OnStartClick;
        public event Action OnGuessClick;
        public event Action OnBackspaceClick;
        
        private List<GuessView> _playerGuessViews = new();
        private List<GuessView> _enemyGuessViews = new();

        private void Awake()
        {
            _startButton.onClick.AddListener(HandleStartClick);
            _guessButton.onClick.AddListener(HandleGuessClick);
        }

        private void HandleStartClick()
        {
            OnStartClick?.Invoke();
        }

        private void HandleGuessClick()
        {
            OnGuessClick?.Invoke();
        }

        public void SetCurrentGuessText(string guess)
        {
            _currentGuessText.text = guess;
        }

        public void SetActiveGuessButton(bool active)
        {
            _guessButton.gameObject.SetActive(active);
        }

        public void SetPlayerGuessesHistory(List<Guess> guesses)
        {
            SetGuessesHistory(guesses, _playerGuessViews, _playerGuessesContainer);
        }
        
        public void SetEnemyGuessesHistory(List<Guess> guesses)
        {
            SetGuessesHistory(guesses, _enemyGuessViews, _enemyGuessesContainer);
        }

        private void SetGuessesHistory(List<Guess> guesses, List<GuessView> guessViews, Transform container)
        {
            int i = 0;
            for (; i < guesses.Count; i++)
            {
                Guess guess = guesses[i];

                if (guessViews.Count == i)
                {
                    GuessView newGuessView = Instantiate(_guessViewPrefab, container);
                    guessViews.Add(newGuessView);
                }
                
                guessViews[i].SetGuess(guess);
                guessViews[i].gameObject.SetActive(true);
            }

            for (; i < guessViews.Count; i++)
            {
                guessViews[i].gameObject.SetActive(false);
            }
        }

        public void ResetView()
        {
            SetCurrentGuessText("");

            List<Guess> emptyGuesses = new();
            SetPlayerGuessesHistory(emptyGuesses);
            SetEnemyGuessesHistory(emptyGuesses);
            _guessButton.gameObject.SetActive(true);
            WinMessage.SetActive(false);
            LoseMessage.SetActive(false);
        }
    }
}