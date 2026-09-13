using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TimerUI : MonoBehaviour
{
    [Header("HUD Text")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Game Over / Leaderboard Panel")]
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private TextMeshProUGUI _topTimesText;

    [Header("End Game Action Panel")]
    [SerializeField] private GameObject _endGameActionsPanel;

    private void Update()
    {
        if (GameTimerManager.Instance != null && GameTimerManager.Instance.IsTimerRunning) _timerText.text = FormatTime(GameTimerManager.Instance.CurrentTime);
    }
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ShowLeaderboard()
    {
        if (_leaderboardPanel != null) _leaderboardPanel.SetActive(true);
        if (_topTimesText != null && GameTimerManager.Instance != null)
        {
            List<float> topTimes = GameTimerManager.Instance.GetTopTimes();
            _topTimesText.text = "<b>TOP 5 TEMPI DI SOPRAVVIVENZA</b>\n\n";
            for (int i = 0; i < topTimes.Count; i++) _topTimesText.text += $"{i + 1}. {FormatTime(topTimes[i])}\n";
        }
    }
    public void OpenEndGameActionsPanel()
    {
        if (_leaderboardPanel != null) _leaderboardPanel.SetActive(false);
        if (_endGameActionsPanel != null) _endGameActionsPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
