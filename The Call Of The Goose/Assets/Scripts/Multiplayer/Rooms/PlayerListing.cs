using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Rooms
{
    public class PlayerListing : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public Player player { get; private set; }
        
        public void SetPlayerInfo(Player player)
        {
            this.player = player;
            _text.text = player.NickName;
        }
    }
}
