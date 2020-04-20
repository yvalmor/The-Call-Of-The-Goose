using Level;
using UnityEngine;

namespace Entities
{
    public class PlayerUI : MonoBehaviour
    {
        private LevelGeneration _level;
        public Minimap _minimap;

        // Start is called before the first frame update
        void Start()
        {
            GameObject go = GameObject.FindWithTag("Level");
            _level = go.GetComponent(typeof(LevelGeneration)) as LevelGeneration;
            _minimap.GenerateMinimap(_level.Rooms);
        }
    }
}
