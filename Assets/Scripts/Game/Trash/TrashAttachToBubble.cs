using GGJ25.Game;
using NUnit.Framework.Internal;
using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashAttachToBubble : MonoBehaviour
    {
        [SerializeField]
        private PlayRandomAudio playRandomAudio;

        private new Rigidbody2D rigidbody;
        private SpriteRenderer spriteRenderer;
        private PolygonCollider2D polygonCollider;

        private BubbleController attachedBubble;
        private Rigidbody2D attachedBubbleRigidbody;
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
                var timeSinceAttached = Mathf.InverseLerp(attachedTime, attachedTime + 1.5f, Time.time);

                rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, attachedBubbleRigidbody.position, Mathf.Lerp(10, 100, timeSinceAttached) * Time.deltaTime));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(attachedBubble == null && collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
            {
                var otherBubble = collision.gameObject.GetComponent<BubbleController>();
                if (otherBubble != null)
                {
                    var otherBubbleMeshFilter = otherBubble.GetComponent<MeshFilter>();

                    if (spriteRenderer.sprite.bounds.size.magnitude < otherBubbleMeshFilter.mesh.bounds.size.magnitude)
                    {
                        otherBubble.AddCaughtTrash(gameObject);

                        polygonCollider = GetComponent<PolygonCollider2D>();
                        polygonCollider.enabled = false;

                        playRandomAudio.Play();

                        attachedBubble = otherBubble;
                        attachedBubbleRigidbody = otherBubble.GetComponent<Rigidbody2D>();

                        rigidbody.linearVelocity = Vector3.zero;
                        attachedTime = Time.time;
                    }
                }
            }
        }
    }
}
