using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Singletons/MasterManager")]
    public class MasterManager : SingletonScriptableObject<MasterManager>
    {
        [SerializeField] private GameSettings _gameSettings;
        public static GameSettings GameSettings => Instance._gameSettings;
    }
}
