using System;
using Level;
using UnityEngine;

namespace Entities
{
    public class PlayerUI : MonoBehaviour
    {
        public Minimap _minimap;

        public void GenerateMinimap()
        {
            GameObject go = GameObject.FindWithTag("Level");
            LevelGeneration level = go.GetComponent<LevelGeneration>();
            _minimap.GenerateMinimap(level.Rooms);
        }
    }
}
