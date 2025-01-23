using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    private Vector2 movementDirection;
    Rigidbody2D trashRigidbody;
    [SerializeField] float movementSpeed = 5f;

    void Start()
    {
        trashRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void Setup(Vector2 direction)
    {
        movementDirection = direction;
        trashRigidbody.linearVelocity = movementDirection * movementSpeed;
    }
}
