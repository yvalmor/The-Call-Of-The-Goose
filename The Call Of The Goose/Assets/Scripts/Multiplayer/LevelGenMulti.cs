using System;
using System.Collections.Generic;
using Level;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Multiplayer
{
    public class LevelGenMulti : MonoBehaviourPun
    {
        public GameObject level;
        public Camera camera;
        
        private void Start()
        {
            Instantiate(level);
            if (PhotonNetwork.IsMasterClient)
            {
                LevelGeneration levelGen = GameObject.FindWithTag("Level").GetComponent<LevelGeneration>();
                levelGen.GenLevel();
                photonView.RPC("SpawnMinimap", RpcTarget.All, levelGen);
                Vector3 pos = levelGen.SpawnRoom;
                pos.x += 15;
                pos.y -= 17.5f;
                photonView.RPC("SpawnPlayer", RpcTarget.All, pos);
            }
        }

        [PunRPC]
        public void SpawnMinimap(LevelGeneration levelGen)
        {
            GameObject.FindWithTag("Minimap").GetComponent<Minimap>().GenerateMinimap(levelGen.Rooms);
        }
        
        [PunRPC]
        public void SpawnPlayer(Vector3 position)
        {
            PhotonNetwork.Instantiate("player", position, Quaternion.identity);
        }
    }
}