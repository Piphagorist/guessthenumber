using System;
using System.Collections.Generic;
using GuessTheNumber.Scripts.Architecture.LocalConfigs;
using GuessTheNumber.Scripts.Patterns.DI;
using Random = UnityEngine.Random;

namespace GuessTheNumber.Scripts.Game.Core
{
    public class GuessCoreManager : SharedObject
    {
        [Inject] private ScriptableObjectsProvider _scriptableObjectsProvider;
        [Inject] private GameHudController _gameHudController;

        public Action<Player> OnWin;

        public GameConfig Config { get; private set; }
        private int _targetValue;
        private HumanPlayer _humanPlayer;
        private AiPlayer _aiPlayer;

        public override void Init()
        {
            Config = _scriptableObjectsProvider.GetConfig<GameConfig>();
                
            _gameHudController.OnStartClick += HandleStartClick;
            
            CreatePlayers();
        }

        private void CreatePlayers()
        {
            _humanPlayer = new();
            GlobalContainer.Instance.InjectAt(_humanPlayer);
            _humanPlayer.Init();
            
            _aiPlayer = new();
            GlobalContainer.Instance.InjectAt(_aiPlayer);
        }

        private void HandleStartClick()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _gameHudController.ResetView();
            _targetValue = Random.Range(Config.MinValue, Config.MaxValue + 1);

            _humanPlayer.Reset();
            _aiPlayer.Reset();
        }

        public GuessResult TryToGuess(int number, Player player)
        {
            GuessResult result = GuessResult.None;

            if (number == _targetValue)
            {
                OnWin?.Invoke(player);
                return GuessResult.Equal;
            }
            
            if (number > _targetValue)
                result = GuessResult.Bigger;
            else if (number < _targetValue)
                result = GuessResult.Smaller;

            Pass(player);
            return result;
        }

        private void Pass(Player currentPlayer)
        {
            currentPlayer.SetLock(true);
            if (_aiPlayer != currentPlayer)
                _aiPlayer.SetLock(false);
            else
                _humanPlayer.SetLock(false);
        }
    }
}