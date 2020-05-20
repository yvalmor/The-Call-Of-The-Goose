using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

namespace Multiplayer.Rooms
{
    public class CreateRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text _roomName;
        [SerializeField] private Text _currentRoomName;
        [SerializeField] private GameObject JoiningRoomMenu, CreateRoomMenu, BeginGameButton;

        public void OnClick_CreateRoom()
        {
            if (_roomName.text.Length == 0 || !PhotonNetwork.IsConnected) return;

            RoomOptions roomOptions = new RoomOptions {MaxPlayers = 2};
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            CreateRoomMenu.SetActive(false);
            JoiningRoomMenu.SetActive(true);
            _currentRoomName.text = $"Current room: {PhotonNetwork.CurrentRoom.Name}";
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room successfully");
            CreateRoomMenu.SetActive(false);
            JoiningRoomMenu.SetActive(true);
            BeginGameButton.SetActive(true);
            _currentRoomName.text = $"Current room: {PhotonNetwork.CurrentRoom.Name}";
        }

        public override void OnLeftRoom()
        {
            Debug.Log("Left room successfully");
            _currentRoomName.text = "Current room: You aren't in any room";
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Room creation failed\n{message}");
        }
    }
}