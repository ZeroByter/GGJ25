using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashReachEndOfScreen : MonoBehaviour
    {
        private Vector3 initialPosition;

        private float deleteX;

        private void Awake()
        {
            initialPosition = transform.position;

            if (initialPosition.x > 0)
            {
                deleteX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
            }
            else
            {
                deleteX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
            }
        }

        private void Update()
        {
            if (initialPosition.x > 0)
            {
                if (transform.position.x < deleteX)
                {
                    Destroy(gameObject);
                    GameManager.Singleton.RemoveLife();
                }
            }
            else
            {
                if (transform.position.x > deleteX)
                {
                    Destroy(gameObject);
                    GameManager.Singleton.RemoveLife();
                }
            }
        }
    }
}
