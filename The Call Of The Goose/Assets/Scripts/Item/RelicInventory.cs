using System;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;

namespace Item
{
    public class RelicInventory : MonoBehaviour
    {
        public List<Relique> list = new List<Relique>();
        public GameObject player;
        public GameObject inventoryPanel;
        public static RelicInventory instance;

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