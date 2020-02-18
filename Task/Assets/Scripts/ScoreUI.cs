using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] Text score = null;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onScoreChanged += UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        score.text = newScore.ToString();
    }
}
