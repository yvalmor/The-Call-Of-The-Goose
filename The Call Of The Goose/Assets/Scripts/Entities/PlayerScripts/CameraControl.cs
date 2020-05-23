using Photon.Pun;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class CameraControl : MonoBehaviourPun
    {
        public bool disabled = false;
        
        public Camera playerCamera;
        private bool _global,
            _previousState,
            _input;

        private void Start()
        {
            _global = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine) return;
            
            Vector3 pos = transform.position;
            pos.z = playerCamera.transform.position.z;
            if (!disabled)
                playerCamera.transform.position = pos;
            else
            {
                pos.x += 9.5f;
                pos.y -= 2f;
                playerCamera.transform.position = pos;
                return;
            }
            
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;
            
            _previousState = _input;
            _input = Input.GetKey(KeyCode.C);
            if (!_input || _input == _previousState) return;
            playerCamera.orthographicSize = _global ? 10f : 100f;
            _global = !_global;
        }
    }
}
