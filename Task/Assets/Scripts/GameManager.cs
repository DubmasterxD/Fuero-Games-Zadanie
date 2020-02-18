using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;

    public delegate void OnScoreChanged(int newScore);
    public event OnScoreChanged onScoreChanged;
    public event Action onGameOver;
    public event Action onGameStart;

    public void AddScore()
    {
        score++;
        onScoreChanged(score);
    }

    public void StartGame()
    {
        score = 0;
        onScoreChanged(score);
        onGameStart();
    }

    public void GameOver()
    {
        onGameOver();
    }
}
