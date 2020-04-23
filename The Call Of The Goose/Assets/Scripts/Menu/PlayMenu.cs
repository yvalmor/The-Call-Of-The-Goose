using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PlayMenu : MonoBehaviour
    {
        public void BeginGame()
        {
            SceneManager.LoadScene("Niveau");
        }

        public void BeginTutorial()
        {
            SceneManager.LoadScene("Tuto");
        }

        public void BeginMulti()
        {
            SceneManager.LoadScene("Multiplayer");
        }
    }
}
