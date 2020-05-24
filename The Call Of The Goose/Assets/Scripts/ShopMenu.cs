using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public void OnClick()
    {
        foreach (GameObject playerObject in GameObject.FindGameObjectsWithTag("playerDeactivated"))
            if (playerObject.GetComponent<Player>().IsMine())
            {
                playerObject.GetComponent<PlayerMovement>().Activate();
                playerObject.GetComponent<CombatEncounter>()._shopActivated = false;
            }
    }
}
