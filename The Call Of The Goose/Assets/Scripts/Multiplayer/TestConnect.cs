using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Multiplayer
{
    public class TestConnect : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        public MasterManager MasterManager;
        
        private void Start()
        {
            Debug.Log("Connecting to master");
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = MasterManager.GameSettings.Username;
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVer;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to server");
            Debug.Log(PhotonNetwork.LocalPlayer.NickName);
            if (!PhotonNetwork.InLobby)
                PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined lobby");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"Disconnected from server for reason: {cause}");
            MasterManager.GameSettings.Username = "Username";
        }
    }
}