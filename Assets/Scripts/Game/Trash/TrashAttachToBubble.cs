using GGJ25.Game;
using NUnit.Framework.Internal;
using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashAttachToBubble : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;
        private SpriteRenderer spriteRenderer;

        private BubbleController attachedBubble;

        private float attachedTime;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (attachedBubble != null)
            {
                var timeSinceAttached = Mathf.InverseLerp(attachedTime, attachedTime + 3, Time.time);

                rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, attachedBubble.transform.position, Mathf.Lerp(10, 100, timeSinceAttached) * Time.deltaTime));
            }

            if (attachedTime != 0 && attachedBubble == null)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var otherBubble = collision.gameObject.GetComponent<BubbleController>();
            if (otherBubble != null && attachedBubble == null)
            {
                var otherBubbleMeshFilter = otherBubble.GetComponent<MeshFilter>();

                if (spriteRenderer.sprite.bounds.size.magnitude < otherBubbleMeshFilter.mesh.bounds.size.magnitude)
                {
                    attachedBubble = otherBubble;
                    rigidbody.linearVelocity = Vector3.zero;
                    attachedTime = Time.time;
                }
            }
        }
    }
}
