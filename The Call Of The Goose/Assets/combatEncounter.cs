using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class combatEncounter : MonoBehaviour
{
    private bool _input;

    private void Start()
    {
        _input = false;
    }

    // Update is called once per frame
    void Update()
    {
        _input = Input.GetKey(KeyCode.K);
        if (_input)
            SceneManager.LoadScene("Combat");
    }
}
