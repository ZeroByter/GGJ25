using UnityEngine;

namespace GGJ25.MainMenu
{
    public class DestroyDiscardedTrash : MonoBehaviour
    {
        private void Update()
        {
            if(transform.position.y < -6)
            {
                Destroy(gameObject);
            }
        }
    }
}
