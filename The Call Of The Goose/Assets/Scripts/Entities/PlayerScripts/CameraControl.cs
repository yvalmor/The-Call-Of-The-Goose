using Photon.Pun;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class CameraControl : MonoBehaviourPun
    {
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
