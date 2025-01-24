using GGJ25.Game;
using NUnit.Framework.Internal;
using UnityEngine;

namespace GGJ25 {
    public class PowerUpAttachToBubble : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private PowerUpController powerUpController;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            powerUpController = GetComponent<PowerUpController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var otherBubble = collision.gameObject.GetComponent<BubbleController>();
            if (otherBubble != null)
            {
                var otherBubbleMeshFilter = otherBubble.GetComponent<MeshFilter>();

                if (spriteRenderer.sprite.bounds.size.magnitude < otherBubbleMeshFilter.mesh.bounds.size.magnitude)
                {
                    Destroy(gameObject);

                    powerUpController.ActivatePower(otherBubble);
                }
            }
        }
    }
}
