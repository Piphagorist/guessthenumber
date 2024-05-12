using System;
using GuessTheNumber.Scripts.Architecture.Assets;
using GuessTheNumber.Scripts.Architecture.Tasks;
using GuessTheNumber.Scripts.Patterns.DI;

namespace GuessTheNumber.Scripts.Game.Core
{
    public class LevelsManager : SharedObject, IExecutable
    {
        public event Action OnGameLoaded;
        
        public LevelHierarchy LevelHierarchy { get; private set; }

        public override void Init()
        {
            LevelHierarchy.OnLoad += HandleHierarchyLoad;
        }

        public ITask Execute()
        {
            return new LoadingSceneFromAddressableTask("Game");
        }

        private void HandleHierarchyLoad(LevelHierarchy hierarchy)
        {
            LevelHierarchy = hierarchy;
            OnGameLoaded?.Invoke();
        }
    }
}