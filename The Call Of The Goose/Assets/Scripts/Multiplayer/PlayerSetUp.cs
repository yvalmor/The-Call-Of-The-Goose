using System;
using Photon.Pun;
using Photon.Realtime;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class PlayerSetUp : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject findOpponent = null;
        [SerializeField] private GameObject waitingStatus = null;
        [SerializeField] private Text waitingText = null;

        private bool isConnect = false;

        private const String Ver = "Oui";
        private const int MaxPlayer = 2;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void FindOpponent()
        {
            isConnect = true;

            findOpponent.SetActive(false);
            waitingStatus.SetActive(true);

            waitingText.text = "Wait...";

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = Ver;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            if (isConnect)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            waitingStatus.SetActive(false);
            findOpponent.SetActive(true);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayer});
        }

        public override void OnJoinedRoom()
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

            if (playerCount != MaxPlayer)
            {
                waitingText.text = "More waiting";
            }
            else
            {
                waitingText.text = "Check make Atheist";
                PhotonNetwork.LoadLevel("Niveau");
            }
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayer)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                waitingText.text = "Check make Atheist";

                PhotonNetwork.LoadLevel("Niveau");
            }

        }
    }

}