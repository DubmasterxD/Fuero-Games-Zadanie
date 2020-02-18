using UnityEngine;

public class Asteroid : MonoBehaviour
{
    bool isDead = false;
    float timeSinceDeath = 0;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SpawnAt(float x, float y)
    {
        transform.position = new Vector3(x, y);
        gameObject.SetActive(true);
        RandomizeMovement();
    }

    private void RandomizeMovement()
    {
        rigidbody.velocity = new Vector2(Random.Range(-1f, 1), Random.Range(-1f, 1)) * Random.Range(0.1f, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Visibility"))
        {
            spriteRenderer.enabled = true;
        }
        else if (collision.CompareTag("Asteroid") || collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Visibility"))
        {
            spriteRenderer.enabled = false;
        }
    }
}
