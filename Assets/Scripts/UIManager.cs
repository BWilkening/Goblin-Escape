using System.Collections;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;

    GameManager gm;
    AudioManager am;

    public static bool GameIsPaused = false;

    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start () {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);
        gm.onRestart.AddListener(ResetUI); // Listen to restart events to reset UI
        am.MenuMusic();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (startMenuUI.activeSelf == false)
        {
            if(gameOverUI.activeSelf == false)
            {
            gm.isPlaying = true;
            am.TransitionMusic();
            }
        }
    }
    
    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gm.isPlaying = false;
        GameIsPaused = true;
        am.MenuMusic();
    }

    public void PlayButtonHandler () {
        gm.StartGame();
        am.ForestMusic();
    }

    public void RestartButtonHandler() {
        gm.RestartGame(); // Call GameManager's RestartGame method
        am.ForestMusic();
    }

    public void ActivateGameOverUI() {
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore();
        gameOverHighScoreUI.text = "High Score: " + gm.PrettyHighScore();
        am.MenuMusic();
    }

    private void ResetUI() {
        gameOverUI.SetActive(false); // Hide the game over UI
        startMenuUI.SetActive(false); // Hide the start menu if you want the game to continue
    }

    private void OnGUI() {
        scoreUI.text = gm.PrettyScore();
        moneyUI.text = gm.PrettyGold();
    }
}
