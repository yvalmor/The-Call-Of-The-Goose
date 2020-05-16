using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Item
{
    public class InventorySlotController : MonoBehaviour
    {
        public Item item;

        private void Start()
        {
            updateInfo();
        }

        public void Use()
        {
            if (item)
            {
                Debug.Log($"You clicked {item.itemName}");
            }
        }

        public void updateInfo()
        {
            Text displayText = transform.Find("Text").GetComponent<Text>();
            Image displayImage = transform.Find("Image").GetComponent<Image>();

            if (item)
            {
                displayText.text = item.itemName;
                displayImage.sprite = item.icon;
                displayImage.color = Color.white;
            }
            else
            {
                displayText.text = "";
                displayImage.sprite = null;
                displayImage.color = Color.clear;

            }
        }
    }
}