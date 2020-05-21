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

        private GameObject[] childs;

        private void Awake()
        {
            if (PhotonNetwork.IsConnected)
                inventoryPanel = GameObject.FindGameObjectWithTag("Inventory panel");
            
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