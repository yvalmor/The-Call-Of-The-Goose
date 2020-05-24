using System;
using System.Collections.Generic;
using System.IO;
using Combat;
using Entities;
using Entities.PlayerScripts;
using Item;
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

        public GameObject Combat,
            RelicsInventory,
            InventoryPanel,
            Inventory,
            shopScreen,
            shopKeeper;
        public BattleSystem battleSystem;

        private void Awake()
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

            Vector3 scale = player.transform.localScale;
            scale *= 1.5f;
            player.transform.localScale = scale;
            
            player.GetComponent<CameraControl>().playerCamera = camera;
            _levelGen = Instantiate(levelPrefab).GetComponent<LevelGeneration>();

            player.GetComponent<CombatEncounter>().combat = Combat;
            player.GetComponent<Inventory>().inventoryPanel = InventoryPanel;
            player.GetComponent<Inventory>().player = player;
            player.GetComponent<RelicInventory>().inventoryPanel = RelicsInventory;
            player.GetComponent<RelicInventory>().player = player;
            player.GetComponent<CombatEncounter>().player = player;
            player.GetComponent<CombatEncounter>().combat = Combat;
            player.GetComponent<CombatEncounter>().inventoryScreen = Inventory;
            player.GetComponent<CombatEncounter>().battleSystem = battleSystem;
            player.GetComponent<CombatEncounter>().shopScreen = shopScreen;
            player.GetComponent<CameraControl>().playerCamera = camera;
            
            shopScreen.SetActive(false);
            Combat.SetActive(false);
            Inventory.SetActive(false);

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
            
            shopKeeper = GameObject.FindWithTag("shopkeeper");
            player.GetComponent<CombatEncounter>().shopKeeper = shopKeeper;
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