using System;
using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject player, pauseMenu;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in players)
            if (playerObject.GetComponent<Player>().IsMine())
                player = playerObject;
    }

    public void Pause()
    {
        player.GetComponent<PlayerMovement>().Activated = false;
        player.tag = "playerDeactivated";
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        player.GetComponent<PlayerMovement>().Activated = true;
        player.tag = "Player";
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }
}
