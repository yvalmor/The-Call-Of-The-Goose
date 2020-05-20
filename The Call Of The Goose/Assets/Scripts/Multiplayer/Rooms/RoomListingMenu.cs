using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Multiplayer.Rooms
{
    public class RoomListingMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _content;
        [SerializeField] private RoomListing _roomListing;
        
        private List<RoomListing> _roomListings = new List<RoomListing>();

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo roomInfo in roomList)
            {
                if (roomInfo.RemovedFromList)
                {
                    int index = _roomListings.FindIndex(x => x.RoomInfo.Name == roomInfo.Name);
                    
                    if (index == -1) continue;
                    
                    Destroy(_roomListings[index].gameObject);
                    _roomListings.RemoveAt(index);
                }
                else
                {
                    RoomListing listing = Instantiate(_roomListing, _content);
                    
                    if (listing == null) continue;
                    
                    listing.SetRoomInfo(roomInfo);
                    _roomListings.Add(listing);
                }
            }
        }
    }
}
