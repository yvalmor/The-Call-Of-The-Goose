using UnityEngine;
using Random = UnityEngine.Random;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Manager/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private string _gameVer = "1.0";
        public string GameVer => _gameVer;

        [SerializeField] private string _username = "Username";

        public string Username
        {
            get => _username == "Username" ? _username + Random.Range(0, 9999) : _username;
            set => _username = value;
        }
    }
}
