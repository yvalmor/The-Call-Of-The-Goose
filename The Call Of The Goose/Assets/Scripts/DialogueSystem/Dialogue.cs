using UnityEngine;

namespace DialogueSystem
{
    [System.Serializable]
    public class Dialogue : MonoBehaviour
    {
        public new string name;
        
        [TextArea(2, 10)]
        public string[] sentences;
    }
}
