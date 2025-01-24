using UnityEngine;

namespace GGJ25.Game
{
    public class BubbleController : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;

        public void Setup(float speed)
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.linearVelocity = new Vector3(0, speed);
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
