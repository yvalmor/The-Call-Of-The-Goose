using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadScene("Lv1");
    }
}
