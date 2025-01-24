using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashReachEndOfScreen : MonoBehaviour
    {
        private Vector3 initialPosition;

        private void Awake()
        {
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (initialPosition.x > 0)
            {
                if (transform.position.x < initialPosition.x * -1f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (transform.position.x > initialPosition.x * -1f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
