using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 3;

    float timeToDeactivate = 0;
    Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        transform.SetPositionAndRotation(position, rotation);
        toggleActive(true);
        rigidbody.velocity = transform.up * bulletSpeed; 
    }

    public void Deactivate(float delay)
    {
        timeToDeactivate = delay;
    }

    private void toggleActive(bool shouldActivate)
    {
        gameObject.SetActive(shouldActivate);
    }
}
