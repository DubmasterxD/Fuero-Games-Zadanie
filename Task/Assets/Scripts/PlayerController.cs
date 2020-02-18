using UnityEngine;

namespace FueroGamesTask.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float acceleration = 1;
        [SerializeField] float deceleration = 0.5f;
        [SerializeField] float maxMovementSpeed = 1;
        [SerializeField] float turnSpeed = 1;

        bool canMove = false;
        GameManager gameManager;
        Rigidbody2D rb2D;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.onGameStart += GameStarted;
            gameManager.onGameOver += GameOver;
            rb2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (canMove)
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
            else
            {
                Decelerate();
                Turn(0);
            }
        }

        private void GameStarted()
        {
            transform.position = new Vector3(0, 0);
            gameObject.SetActive(true);
            canMove = true;
        }

        private void GameOver()
        {
            canMove = false;
        }

        private void Accelerate()
        {
            rb2D.velocity += (Vector2)transform.up * acceleration;
            float currentMovementSpeed = rb2D.velocity.magnitude;
            if (currentMovementSpeed > maxMovementSpeed)
            {
                rb2D.velocity = rb2D.velocity / (currentMovementSpeed / maxMovementSpeed);
            }
        }

        private void Decelerate()
        {
            rb2D.velocity -= rb2D.velocity * deceleration;
            float currentMovementSpeed = rb2D.velocity.magnitude;
            if (currentMovementSpeed < 0)
            {
                rb2D.velocity = new Vector2(0, 0);
            }
        }

        private void Turn(float speed)
        {
            rb2D.angularVelocity = speed;
        }
    }
}