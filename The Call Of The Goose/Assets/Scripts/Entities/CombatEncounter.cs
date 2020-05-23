using System;
using Entities.PlayerScripts;
using Level;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class CombatEncounter : MonoBehaviourPun
    {
        public bool _combatInput, _quitInput, _nextLevelInput, _shopInput,
            _inventoryInput, _inventoryActivated, _shopActivated;
        public GameObject player, inventoryScreen, combat, shopKeeper, shopScreen;
        public PlayerMovement PlayerMovement;

        private void Awake()
        {
            foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (playerObject.GetComponent<Player>().IsMine())
                    player = playerObject;
            }
            
            if (PhotonNetwork.IsConnected && photonView.IsMine)
            {
                inventoryScreen = GameObject.Find("Inventory Screen");
                shopScreen = GameObject.Find("ShopMenu");
            }
            shopKeeper = GameObject.Find("Shopkeeper");
        }

        private void Start()
        {
            inventoryScreen.SetActive(false);
            _inventoryActivated = false;
        }

        private void UpdateInputs()
        {
            _combatInput = Input.GetKeyDown(KeyCode.K);
            _quitInput = Input.GetKeyDown(KeyCode.Escape);
            _nextLevelInput = Input.GetKeyDown(KeyCode.N);
            _inventoryInput = Input.GetKeyDown(KeyCode.E);
            _shopInput = Input.GetKeyDown(KeyCode.S);
        }

        private void ExecuteEvents()
        {
            if (_combatInput)
            {
                PlayerMovement.Deactivate();
                combat.SetActive(true);
            }
            
            else if (_shopInput && !_shopActivated && 
                     Vector3.Distance(player.transform.position, shopKeeper.transform.position) < 5)
            {
                _shopActivated = true;
                PlayerMovement.Deactivate();
                shopScreen.SetActive(true);
            } else if (_shopInput)
            {
                _shopActivated = false;
                PlayerMovement.Activate();
                shopScreen.SetActive(false);
            }
        
            else if (_quitInput)
                SceneManager.LoadScene("Menu");
        
            else if (_inventoryInput)
            {
                if (!_inventoryActivated)
                {
                    PlayerMovement.Deactivate();
                    inventoryScreen.SetActive(true);
                    _inventoryActivated = true;
                }
            
                else
                {
                    PlayerMovement.Activate();
                    inventoryScreen.SetActive(false);
                    _inventoryActivated = false;
                }
            }
        
            else if (_nextLevelInput)
            {
                LevelGeneration level = FindObjectOfType<LevelGeneration>();
                level.DestroyLevel();
                level.GenLevel();
            }
        }
    
        // Update is called once per frame
        private void Update()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;
        
            UpdateInputs();
            ExecuteEvents();
        }
    }
}
