using UnityEngine;

namespace GGJ25.Game
{
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]
        private float floatSpeed = 5f;

        private new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.linearVelocity = new Vector3(0, floatSpeed);
        }

        private void Update()
        {
            if (transform.position.y > 18f)
            {
                Destroy(gameObject);
            }
        }
    }
}
