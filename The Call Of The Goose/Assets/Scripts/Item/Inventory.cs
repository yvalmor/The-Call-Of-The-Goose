using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> list = new List<Item>();
        public GameObject player;
        public GameObject inventoryPanel;
        public static Inventory instance;

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

        public void Add(Item item)
        {
            if (list.Count < 6)
            {
                list.Add(item);
            }
            updatePanelSlots();
        }

        public void Remove(Item item)
        {
            list.Remove(item);
            updatePanelSlots();
        }
        
    }
}