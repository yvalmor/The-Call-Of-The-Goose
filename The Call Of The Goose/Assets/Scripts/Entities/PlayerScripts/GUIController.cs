using System;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class GUIController : MonoBehaviour
    {
        public GameObject[] myButtons;

        public static GUIController singleton = null;

        void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(this.gameObject);
            }

            else if (singleton != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            myButtons = GameObject.FindGameObjectsWithTag("ItemsShop");
        }
    }
}