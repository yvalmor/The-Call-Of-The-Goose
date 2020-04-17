using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}