using Photon.Pun;
using UnityEngine;

namespace Multiplayer.Rooms
{
    public class LeaveRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject JoiningRoomMenu, CreatingRoomMenu;
        
        public void OnClick_LeaveRoom()
        {
            if (!PhotonNetwork.InRoom) return;

            PhotonNetwork.LeaveRoom();
            JoiningRoomMenu.SetActive(false);
            CreatingRoomMenu.SetActive(true);
        }
    }
}
