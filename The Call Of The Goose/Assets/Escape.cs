using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    private bool _quit;

    // Start is called before the first frame update
    void Start()
    {
        _quit = false;
    }

    // Update is called once per frame
    void Update()
    {
        _quit = Input.GetKey(KeyCode.Escape);
        if (_quit)
            SceneManager.LoadScene("Menu");
    }
}
