using UnityEngine;

namespace GGJ25 {
    public class PowerUpMovement : MonoBehaviour
    {
        private Vector2 movementDirection;
        Rigidbody2D powerUpRigidbody;
        [SerializeField] float movementSpeed = 5f;

        void Awake()
        {
            powerUpRigidbody = GetComponent<Rigidbody2D>();
        }

        public void Setup(Vector2 direction)
        {
            movementDirection = direction;
            powerUpRigidbody.linearVelocity = movementDirection * movementSpeed;
        }
    }
}