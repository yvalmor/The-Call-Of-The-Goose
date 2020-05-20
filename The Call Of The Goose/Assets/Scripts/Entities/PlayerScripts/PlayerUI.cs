using System;
using Level;
using Photon.Pun;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class PlayerUI : MonoBehaviour
    {
        public Minimap _minimap;

        private void Start()
        {
            if (!PhotonNetwork.IsConnected)
                GenerateMinimap();
        }

        public void GenerateMinimap()
        {
            GameObject go = GameObject.FindWithTag("Level");
            LevelGeneration level = go.GetComponent<LevelGeneration>();
            _minimap.GenerateMinimap(level.Rooms);
        }
    }
}
