using UnityEngine;

public class CreatingAsteroidsUI : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<GameManager>().onGameStart += FinishedCreating;
    }

    private void FinishedCreating()
    {
        gameObject.SetActive(false);
    }
}
