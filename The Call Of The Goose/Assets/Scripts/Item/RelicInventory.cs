using System;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Photon.Pun;
using UnityEngine;

namespace Item
{
    public class RelicInventory : MonoBehaviour
    {
        public List<Relique> list = new List<Relique>();
        public GameObject player;
        public GameObject inventoryPanel;
        public static RelicInventory instance;

        private GameObject[] childs;

        private void Awake()
        {
            if (PhotonNetwork.IsConnected)
                inventoryPanel = GameObject.FindWithTag("Relic panel");

            childs = new GameObject[inventoryPanel.transform.childCount];
            
            for (int i = 0; i < inventoryPanel.transform.childCount; i++)
                childs[i] = inventoryPanel.transform.GetChild(i).gameObject;
        }

        private void Start()
        {
            instance = this;
            updatePanelSlots();
        }

        private void updatePanelSlots()
        {
            int index = 0;
            foreach (GameObject child in childs)
            {
                InventorySlotController slot = child.GetComponent<InventorySlotController>();
                slot.item = index < list.Count ? list[index] : null;

                slot.updateInfo();
                index++;
            }
        }

        public void Add(Relique relique)
        {
            if (list.Count < 10)
            {
                list.Add(relique);
                relique.Gain(player.GetComponent<Player>());
            }
            updatePanelSlots();
        }

        public void Remove(Relique relique)
        {
            list.Remove(relique);
            updatePanelSlots();
        }
        
    }
}