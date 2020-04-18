using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatEncounter : MonoBehaviour
{
    private bool _input, _quit, _next;
    public LevelGeneration level;

    private void Start()
    {
        _input = false;
        _quit = false;
    }

    // Update is called once per frame
    private void Update()
    {
        _input = Input.GetKey(KeyCode.K);
        if (_input)
            SceneManager.LoadScene("Combat");
        _quit = Input.GetKey(KeyCode.Escape);
        if (_quit)
            SceneManager.LoadScene("Menu");
        _next = Input.GetKey(KeyCode.N);
        if (!_next) return;
        level.DestroyLevel();
        level.GenLevel();
    }
}
