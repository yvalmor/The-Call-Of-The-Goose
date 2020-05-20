using System.Collections;
using System.Collections.Generic;
using Multiplayer;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameChoice : MonoBehaviour
{
    [SerializeField] private Text _text;

    public void OnClick_UsernameChoice()
    {
        string text = _text.text;

        if (text == "") return;
        MasterManager.GameSettings.Username = text;
        PhotonNetwork.NickName = text;
    }
}
