using UnityEngine;

public class Asteroid : MonoBehaviour
{
    bool isDead = false;
    float timeSinceDeath = 0;
    Rigidbody2D rigidbody;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        RandomizeMovement();
    }

    private void RandomizeMovement()
    {
        rigidbody.velocity = new Vector2(Random.Range(-1f, 1), Random.Range(-1f, 1)) * Random.Range(0.1f, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Visibility"))
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Visibility"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (collision.CompareTag("Bounds"))
        {
            transform.position = collision.transform.position + collision.transform.position - transform.position;
        }
    }
}
