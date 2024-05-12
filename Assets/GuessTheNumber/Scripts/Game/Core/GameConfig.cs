using GuessTheNumber.Scripts.Patterns.Singleton;
using UnityEngine;

namespace GuessTheNumber.Scripts.Game.Core
{
    [CreateAssetMenu(order = 0, fileName = "GameConfig", menuName = "GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        [field: SerializeField] public int MinValue { get; private set; }
        [field: SerializeField] public int MaxValue { get; private set; }
    }
}