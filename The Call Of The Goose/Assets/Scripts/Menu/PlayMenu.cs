using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PlayMenu : MonoBehaviour
    {
        public void BeginGame()
        {
            SceneManager.LoadScene("Lv1");
        }

        public void BeginTutorial()
        {
            SceneManager.LoadScene("Tuto");
        }
    }
}
