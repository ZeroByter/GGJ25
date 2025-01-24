using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashMovement : MonoBehaviour
    {
        private Vector2 movementDirection;
        Rigidbody2D trashRigidbody;
        [SerializeField] float movementSpeed = 5f;

        void Awake()
        {
            trashRigidbody = GetComponent<Rigidbody2D>();
        }

        public void Setup(Vector2 direction)
        {
            movementDirection = direction;
            trashRigidbody.linearVelocity = movementDirection * movementSpeed;
        }
    }
}