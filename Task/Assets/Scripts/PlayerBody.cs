using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onGameStart += GameStarted;
        gameManager.onGameStart += GameOver;
    }

    private void GameStarted()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void GameOver()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            gameManager.GameOver();
        }
    }
}
