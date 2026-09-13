using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameTimerManager : MonoBehaviour
{
    public static GameTimerManager Instance { get; private set; }
    public float CurrentTime { get; private set; }
    public bool IsTimerRunning { get; private set; }

    private const int MaxTopTimes = 5;
    private const string LeaderboardKeyPrefix = "TopTime_";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        Time.timeScale = 1f;
        StartTimer();
    }
    private void Update()
    {
        if (IsTimerRunning) CurrentTime += Time.deltaTime;
    }
    public void StartTimer()
    {
        CurrentTime = 0f;
        IsTimerRunning = true;
    }
    public void StopTimerAndSaveRecord()
    {
        if (!IsTimerRunning) return;
        IsTimerRunning = false;
        SaveTimeRecord(CurrentTime);
    }
    private void SaveTimeRecord(float newTime)
    {
        List<float> topTimes = GetTopTimes();
        topTimes.Add(newTime);
        topTimes.Sort((a, b) => b.CompareTo(a));
        if (topTimes.Count > MaxTopTimes) topTimes = topTimes.GetRange(0, MaxTopTimes);
        for (int i = 0; i < topTimes.Count; i++) PlayerPrefs.SetFloat(LeaderboardKeyPrefix + i, topTimes[i]);
        PlayerPrefs.Save();
    }
    public List<float> GetTopTimes()
    {
        List<float> topTimes = new List<float>();
        for (int i = 0; i < MaxTopTimes; i++)
        {
            if (PlayerPrefs.HasKey(LeaderboardKeyPrefix + i)) topTimes.Add(PlayerPrefs.GetFloat(LeaderboardKeyPrefix + i));
        }
        return topTimes;
    }
}
