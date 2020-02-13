using UnityEngine;

namespace FueroGamesTask.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float acceleration = 1;
        [SerializeField] float deceleration = 0.5f;
        [SerializeField] float maxMovementSpeed = 1;
        [SerializeField] float turnSpeed = 1;

        Vector3 currentVelocity = new Vector3(0, 0, 0);

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
            Move();
        }

        private void Accelerate()
        {
            currentVelocity += transform.up * acceleration;
            float currentMovementSpeed = currentVelocity.magnitude;
            if (currentMovementSpeed > maxMovementSpeed)
            {
                currentVelocity = currentVelocity / (currentMovementSpeed / maxMovementSpeed);
            }
        }

        private void Decelerate()
        {
            currentVelocity -= currentVelocity * deceleration;
            float currentMovementSpeed = currentVelocity.magnitude;
            if (currentMovementSpeed < 0)
            {
                currentVelocity = new Vector3(0, 0, 0);
            }
        }

        private void Move()
        {
            transform.localPosition += currentVelocity * Time.deltaTime;
        }

        private void Turn(float speed)
        {
            transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
        }
    }
}