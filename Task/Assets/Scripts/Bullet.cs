using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 3;

    float timeToDeactivate = 0;
    Rigidbody2D rb2d;
    GameManager gameManager;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        timeToDeactivate -= Time.deltaTime;
        if (timeToDeactivate < 0)
        {
            toggleActive(false);
        }
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        toggleActive(true);
        rb2d.velocity = transform.up * bulletSpeed; 
    }

    public void DeactivateIn(float delay)
    {
        timeToDeactivate = delay;
    }

    private void toggleActive(bool shouldActivate)
    {
        gameObject.SetActive(shouldActivate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            toggleActive(false);
            gameManager.AddScore();
        }
    }
}
