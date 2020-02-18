using UnityEngine;

public class RestartUI : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onGameOver += GameOver;
        gameObject.SetActive(false);
    }

    private void GameOver()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameManager.StartGame();
        gameObject.SetActive(false);
    }
}
