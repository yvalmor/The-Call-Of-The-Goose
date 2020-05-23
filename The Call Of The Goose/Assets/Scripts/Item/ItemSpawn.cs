using Entities.PlayerScripts;
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
                if (player.IsMine())
                    player.BuyItem(choosen);
            }
        }
    }
}
