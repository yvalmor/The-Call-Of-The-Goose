﻿using System;
using System.Collections.Generic;
using System.IO;
using Entities.PlayerScripts;
using Level;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Multiplayer
{
    public class LevelGenMulti : MonoBehaviourPun
    {
        public GameObject playerPrefab;
        public GameObject levelPrefab;
        public GameObject player;
        public new Camera camera;
        private LevelGeneration _levelGen;

        private void Start()
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            player.GetComponent<CameraControl>().playerCamera = camera;
            _levelGen = Instantiate(levelPrefab).GetComponent<LevelGeneration>();
            
            if (PhotonNetwork.IsMasterClient)
            {
                _levelGen.GenLevel();
                
                
                Vector3 pos = _levelGen.SpawnRoom;
                pos.x += 17.5f;
                pos.y -= 17.5f;
                player.transform.position = pos;
                
                MemoryStream ms = new MemoryStream(12);
                ms.Write(BitConverter.GetBytes(player.transform.position.x), 0, 4);
                ms.Write(BitConverter.GetBytes(player.transform.position.y), 0, 4);
                ms.Write(BitConverter.GetBytes(player.transform.position.z), 0, 4);
                
                photonView.RPC("SpawnPlayer", RpcTarget.Others, ms.ToArray());
            }
        }

        [PunRPC]
        public void SpawnPlayer(byte[] bytes)
        {
            Vector3 spawn;
			
            spawn.x = BitConverter.ToSingle(bytes, 0);
            spawn.y = BitConverter.ToSingle(bytes, 4);
            spawn.z = BitConverter.ToSingle(bytes, 8);

            player.transform.position = spawn;
        }
    }
}