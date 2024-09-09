using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Menus & Screens")]
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject victoryCanvas;
    [Space(20)]

    public static UnityEvent<int> onScoreChange = new UnityEvent<int>();
    public static UnityEvent onResetGame = new UnityEvent();
    public static UnityEvent onPauseMenu = new UnityEvent();
    public static UnityEvent onResumeGame = new UnityEvent();
    public static UnityEvent onWinGame = new UnityEvent();

    public static int passedObstaclesCount;

    public static float gameTime;

    private static bool _gameActive = true;

    [Header("Debug")] 
    [SerializeField] private bool winGame;

    private void Start()
    {
        Time.timeScale = 1f;
        passedObstaclesCount = 0;   
    }

    private void Update()
    {
        if(_gameActive)
        {
            gameTime += Time.deltaTime;
        }
        
        if(winGame)
        {
            WinGame();
        }
    }

    public static void IncrementNumberPassedObstacles()
    {
        passedObstaclesCount++;
        onScoreChange?.Invoke(passedObstaclesCount);
    }

    public static void ResetGame()
    {
        Time.timeScale = 1f;
        onResetGame?.Invoke();
        passedObstaclesCount = 0;
        onScoreChange?.Invoke(passedObstaclesCount);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
        onPauseMenu.Invoke();
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        onResumeGame.Invoke();
    }

    private void WinGame()
    {
        _gameActive = false;
        Time.timeScale = 0f;
        victoryCanvas.SetActive(true);
        onWinGame.Invoke();
    }
}
