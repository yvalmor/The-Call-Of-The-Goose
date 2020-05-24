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

        public void Disable()
        {
            disabled = true;
            
            Vector3 pos = transform.position;
            pos.z = playerCamera.transform.position.z;
            pos.x += 11f;
            pos.y += 1.5f;
            
            playerCamera.transform.position = pos;
            
            Vector3 scale = transform.localScale;
            scale.x *= 1.5f;
            scale.y *= 1.5f;
            transform.localScale = scale;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (disabled || PhotonNetwork.IsConnected && !photonView.IsMine) return;

            Vector3 pos = transform.position;
            pos.z = playerCamera.transform.position.z;
            playerCamera.transform.position = pos;
            
            
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
