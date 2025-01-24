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
                particleSystemShape.texture = chosenSprite.texture;
            }

            var newCollider = gameObject.AddComponent<PolygonCollider2D>();
            newCollider.isTrigger = colliderTrigger;
        }
    }
}
