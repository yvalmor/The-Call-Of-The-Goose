using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Item
{
    public class Inventory : MonoBehaviour
    {
        public List<Consumable> list = new List<Consumable>();
        public GameObject player;
        public GameObject inventoryPanel;
        public static Inventory instance;

        private void Awake()
        {
            if (PhotonNetwork.IsConnected)
                inventoryPanel = GameObject.FindGameObjectWithTag("Inventory Panel");
        }

        private void Start()
        {
            instance = this;
            updatePanelSlots();
        }

        private void updatePanelSlots()
        {
            int index = 0;
            foreach (Transform child in inventoryPanel.transform)
            {
                InventorySlotController slot = child.GetComponent<InventorySlotController>();
                if (index < list.Count)
                    slot.item = list[index];
                else
                    slot.item = null;

                slot.updateInfo();
                index++;
            }
        }

        public void Add(Consumable consumable)
        {
            if (list.Count < 6)
            {
                list.Add(consumable);
            }
            updatePanelSlots();
        }

        public void Remove(Consumable consumable)
        {
            list.Remove(consumable);
            updatePanelSlots();
        }
        
    }
}