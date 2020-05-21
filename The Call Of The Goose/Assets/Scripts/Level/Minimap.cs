using System;
using Photon.Pun;
using UnityEngine;

namespace Level
{
    public class Minimap : MonoBehaviour
    {
        public GameObject playerPosition;
        public Transform[] positions;
        public GameObject[] mapRooms;
        public GameObject playerIcon;

        public void GenerateMinimap(Room[] rooms)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                GameObject r = getMapRoom(rooms[i]);
                r.layer = 5;
                Instantiate(r, positions[i].position, Quaternion.identity, transform);
            }
        }

        public void SetPlayer(GameObject player) => playerPosition = player;

        private GameObject getMapRoom(Room room)
        {
            if (!room.down && !room.up && !room.left && !room.right)
                return mapRooms[0];
            if (!room.down && !room.left && !room.right)
                return mapRooms[1];
            if (!room.up && !room.down && !room.left)
                return mapRooms[2];
            if (!room.up && !room.down && !room.right)
                return mapRooms[3];
            if (!room.up && !room.left && !room.right)
                return mapRooms[4];
            if (!room.down && !room.left)
                return mapRooms[5];
            if (!room.down && !room.right)
                return mapRooms[6];
            if (!room.left && !room.right)
                return mapRooms[7];
            if (!room.up && !room.down)
                return mapRooms[8];
            if (!room.up && !room.left)
                return mapRooms[9];
            if (!room.up && !room.right)
                return mapRooms[10];
            if (!room.down)
                return mapRooms[11];
            if (!room.left)
                return mapRooms[12];
            return !room.right ? mapRooms[13] : mapRooms[14];
        }

        private void UpdatePlayerIconPosition()
        {
            Vector3 position = playerPosition.transform.position;

            position.x /= 31;
            
            position.y /= -35;

            int index = (int) position.y * 4 + (int) position.x;
            
            Transform newPos = positions[index];

            Destroy(playerIcon);

            playerIcon = Instantiate(playerIcon, newPos);
        }

        private void Update()
        {
            if (!PhotonNetwork.IsConnected || playerPosition.GetPhotonView().IsMine)
                UpdatePlayerIconPosition();
        }
    }
}