using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
    }

    #endregion

    public float currentScore = 0f;
    public SaveData data;
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onRestart = new UnityEvent(); // New UnityEvent for restart

    private void Start() {
        data = new SaveData();
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null) {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        } else {
            data = new SaveData();
        }
    }

    private void Update() {
        if (isPlaying) {
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame() {
        isPlaying = true;
        currentScore = 0;
        onPlay.Invoke();
        onRestart.Invoke();
    }

    public void GameOver() {
        if (data.highscore < currentScore) {
            data.highscore = currentScore;
            string saveScore = JsonUtility.ToJson(data);
            SaveSystem.Save("save", saveScore);
        }
        isPlaying = false;

        onGameOver.Invoke();
    }

     public void RestartGame() {
         // Reset game state and broadcast the restart event
         currentScore = 0;
         isPlaying = true;
         onRestart.Invoke(); // Call listeners to reset player health and other elements
         onPlay.Invoke(); // Optionally re-trigger play events
     }

     public void Bank (int coin) {
        data.money += coin;
        string saveCoin = JsonUtility.ToJson(data);
            SaveSystem.Save("save", saveCoin);
     }

    public string PrettyScore() {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighScore() {
        return Mathf.RoundToInt(data.highscore).ToString();
    }
    public string PrettyGold() {
        return Mathf.RoundToInt(data.money).ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
    
