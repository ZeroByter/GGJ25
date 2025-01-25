using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class RandomTrashSprite : MonoBehaviour
    {
        [SerializeField]
        private bool colliderTrigger;
        [SerializeField]
        private Sprite[] sprites;

        private SpriteRenderer spriteRenderer;
        private new ParticleSystem particleSystem;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            particleSystem = GetComponent<ParticleSystem>();

            var chosenSprite = sprites[Random.Range(0, sprites.Length)];

            spriteRenderer.sprite = chosenSprite;

            if (particleSystem)
            {
                var particleSystemShape = particleSystem.shape;
                particleSystemShape.scale = new Vector3(chosenSprite.bounds.size.x, chosenSprite.bounds.size.y, 1);
            }

            var newCollider = gameObject.AddComponent<PolygonCollider2D>();
            newCollider.isTrigger = colliderTrigger;
        }
    }
}
