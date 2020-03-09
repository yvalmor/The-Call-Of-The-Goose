using UnityEngine;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public void QuitGame()
        {
            Debug.Log("Closing Game");
            Application.Quit();
        }
    }
}
