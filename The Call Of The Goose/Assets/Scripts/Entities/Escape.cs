using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities
{
    public class Escape : MonoBehaviourPun
    {
        private bool _quit;

        // Start is called before the first frame update
        void Start()
        {
            _quit = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;
        
            _quit = Input.GetKey(KeyCode.Escape);
            if (_quit)
                SceneManager.LoadScene("Menu");
        }
    }
}
