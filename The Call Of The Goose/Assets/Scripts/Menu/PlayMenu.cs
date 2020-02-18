using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void BeginGame()
    {
        SceneManager.LoadScene("Game");
    }
}
