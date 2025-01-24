using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class RandomTrashSprite : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites;

        private BoxCollider2D boxCollider;
        private SpriteRenderer spriteRenderer;
        private new ParticleSystem particleSystem;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            particleSystem = GetComponent<ParticleSystem>();

            var chosenSprite = sprites[Random.Range(0, sprites.Length)];

            spriteRenderer.sprite = chosenSprite;

            boxCollider.size = chosenSprite.bounds.size;

            var particleSystemShape = particleSystem.shape;
            particleSystemShape.texture = chosenSprite.texture;
        }
    }
}
