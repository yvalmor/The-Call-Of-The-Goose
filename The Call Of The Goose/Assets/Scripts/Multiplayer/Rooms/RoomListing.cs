﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Rooms
{
    public class RoomListing : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public RoomInfo RoomInfo { get; private set; }
        
        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            _text.text = $"{roomInfo.Name}: {roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
        }

        public void OnClick_Button()
        {
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        }
    }
}
