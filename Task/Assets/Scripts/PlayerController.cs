using UnityEngine;

namespace FueroGamesTask.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float acceleration = 1;
        [SerializeField] float deceleration = 0.5f;
        [SerializeField] float maxMovementSpeed = 1;
        [SerializeField] float turnSpeed = 1;
        
        Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                Accelerate();
            }
            else
            {
                Decelerate();
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                Turn(turnSpeed);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                Turn(-turnSpeed);
            }
            else
            {
                Turn(0);
            }
        }

        private void Accelerate()
        {
            rigidbody.velocity += (Vector2)transform.up * acceleration;
            float currentMovementSpeed = rigidbody.velocity.magnitude;
            if (currentMovementSpeed > maxMovementSpeed)
            {
                rigidbody.velocity = rigidbody.velocity / (currentMovementSpeed / maxMovementSpeed);
            }
        }

        private void Decelerate()
        {
            rigidbody.velocity -= rigidbody.velocity * deceleration;
            float currentMovementSpeed = rigidbody.velocity.magnitude;
            if (currentMovementSpeed < 0)
            {
                rigidbody.velocity = new Vector2(0, 0);
            }
        }

        private void Turn(float speed)
        {
            rigidbody.angularVelocity = speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Asteroid"))
            {
                //gameover
            }
        }
    }
}