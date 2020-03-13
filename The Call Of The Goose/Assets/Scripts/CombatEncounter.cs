using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEncounter : MonoBehaviour
{
    private bool _input, _quit;

    private void Start()
    {
        _input = false;
        _quit = false;
    }

    // Update is called once per frame
    void Update()
    {
        _input = Input.GetKey(KeyCode.K);
        if (_input)
            SceneManager.LoadScene("Combat");
        _quit = Input.GetKey(KeyCode.Escape);
        if (_quit)
            SceneManager.LoadScene("Menu");
    }
}
