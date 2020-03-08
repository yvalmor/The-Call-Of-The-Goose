using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MAIN_MENU,
    OPTION_MENU,
    PLAY_MENU,
    TUTORIAL,
    PLAY,
    PAUSE,
    GAME_OVER
};

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager(){}
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
            {
                DontDestroyOnLoad(GameManager.instance);
                GameManager.instance = new GameManager();
            }

            return GameManager.instance;
        }
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }

    /*// Start is called before the first frame update
    void Start()
    {
        runMenuScene();
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        throw new NotImplementedException();
    }*/

    public static void runMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
