using System;
using Combat;
using Entities.PlayerScripts;
using Level;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class CombatEncounter : MonoBehaviourPun
    {
        public Animator Animator;
        
        public bool _quitInput, _nextLevelInput, _shopInput,
            _inventoryInput, _inventoryActivated, _shopActivated;
        public GameObject player, inventoryScreen, combat, shopKeeper, shopScreen;
        public PlayerMovement PlayerMovement;
        public BattleSystem battleSystem;
        
        private static readonly int fighting = Animator.StringToHash("fighting");

        private void Start()
        {
            if (PhotonNetwork.IsConnected && !player.GetComponent<Player>().IsMine())
                return;
            if (!PhotonNetwork.IsConnected)
            {
                shopKeeper = GameObject.FindWithTag("shopkeeper");
                shopScreen.SetActive(false);
                inventoryScreen.SetActive(false);
            }
            _inventoryActivated = false;
            EndFight();
        }

        private void UpdateInputs()
        {
            _quitInput = Input.GetKeyDown(KeyCode.Escape);
            _nextLevelInput = Input.GetKeyDown(KeyCode.N);
            _inventoryInput = Input.GetKeyDown(KeyCode.E);
            _shopInput = Input.GetKeyDown(KeyCode.S);
        }

        private void ExecuteEvents()
        {
            if (_shopInput && !_shopActivated && 
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

        public void BeginFight(Ennemy ennemy)
        {
            player.GetComponent<CameraControl>().Disable();
            Vector3 scale = player.transform.localScale;
            scale.x *= 1.5f;
            scale.y *= 1.5f;
            player.transform.localScale = scale;

            Animator.SetBool(fighting, true);
            player.GetComponent<PlayerMovement>().Deactivate();
            player.GetComponent<SpriteRenderer>().sortingLayerName = "Fight";
            battleSystem.SetFighters(player.GetComponent<Player>(), ennemy);
            combat.SetActive(true);
        }

        public void EndFight()
        {
            player.GetComponent<CameraControl>().disabled = false;
            Vector3 scale = player.transform.localScale;
            scale.x /= 1.5f;
            scale.y /= 1.5f;
            player.transform.localScale = scale;
            
            Animator.SetBool(fighting, false);
            player.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            player.GetComponent<PlayerMovement>().Activate();
            combat.SetActive(false);
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
