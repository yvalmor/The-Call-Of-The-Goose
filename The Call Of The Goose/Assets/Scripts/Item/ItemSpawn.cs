using Entities.PlayerScripts;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Item
{
    public class ItemSpawn : MonoBehaviour
    {
        public Item[] items;
        public Item choosen;

        public Button Button;
        public Image Image;
        public TMP_Text price;

        private void Awake()
        {
            choosen = items[Random.Range(0, items.Length)];
            Instantiate(choosen, transform);
            Image.sprite = choosen.icon;
            price.text = choosen.price.ToString();
        }

        public void Buy()
        {
            foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("playerDeactivated"))
            {
                Player player = playerObject.GetComponent<Player>();
                if (PhotonNetwork.IsConnected && !player.IsMine()) continue;
                
                player.BuyItem(choosen);

                Debug.Log($"Added {(choosen != null ? choosen.itemName : null)} to inventory");

                choosen = null;
                price.text = "Unavailable";
                Button.interactable = false;
            }
        }

        public void Reset()
        {
            Awake();
        }
    }
}