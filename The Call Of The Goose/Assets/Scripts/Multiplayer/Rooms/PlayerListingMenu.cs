using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Multiplayer.Rooms
{
    public class PlayerListingMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _content;
        [SerializeField] private PlayerListing _playerListing;
        
        private List<PlayerListing> _playerListings = new List<PlayerListing>();

        private void Awake()
        {
            GetCurrentRoomPlayers();
        }

        private void GetCurrentRoomPlayers()
        {
            if (!PhotonNetwork.IsConnected || 
                PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
                return;
            
            foreach (KeyValuePair<int,Player> currentRoomPlayer in PhotonNetwork.CurrentRoom.Players)
                AddPlayerListing(currentRoomPlayer.Value);
        }

        private void AddPlayerListing(Player player)
        {
            PlayerListing listing = Instantiate(_playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _playerListings.Add(listing);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int index = _playerListings.FindIndex(x => x.player == otherPlayer);
            
            if (index == -1) return;
            
            Destroy(_playerListings[index].gameObject);
            _playerListings.RemoveAt(index);
        }
    }
}