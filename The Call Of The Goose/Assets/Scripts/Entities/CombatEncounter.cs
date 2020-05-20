using Entities.PlayerScripts;
using Level;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class CombatEncounter : MonoBehaviourPun
    {
        private bool _combatInput, _quitInput, _nextLevelInput,
            _inventoryInput, _inventoryActivated, _previousInventoryInput;
        public GameObject player, inventoryScreen, combat;
        public Camera playerCamera;
        public PlayerMovement PlayerMovement;

        private void Start()
        {
            _inventoryActivated = false;
        }

        private void UpdateInputs()
        {
            _previousInventoryInput = _inventoryInput;
            _combatInput = Input.GetKey(KeyCode.K);
            _quitInput = Input.GetKey(KeyCode.Escape);
            _nextLevelInput = Input.GetKey(KeyCode.N);
            _inventoryInput = Input.GetKey(KeyCode.E);
        }

        private void ExecuteEvents()
        {
            if (_combatInput)
            {
                PlayerMovement.Activated = false;
                player.tag = "playerDeactivated";
                combat.SetActive(true);
                playerCamera.gameObject.SetActive(false);
            }
        
            else if (_quitInput)
                SceneManager.LoadScene("Menu");
        
            else if (_inventoryInput && _inventoryInput != _previousInventoryInput)
            {
                if (!_inventoryActivated)
                {
                    PlayerMovement.Activated = false;
                    player.tag = "playerDeactivated";
                    inventoryScreen.SetActive(true);
                    playerCamera.gameObject.SetActive(false);
                    _inventoryActivated = true;
                }
            
                else
                {
                    PlayerMovement.Activated = true;
                    player.tag = "Player";
                    inventoryScreen.SetActive(false);
                    playerCamera.gameObject.SetActive(true);
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
