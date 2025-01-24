using System;
using System.Runtime.CompilerServices;
using GGJ25.Game.Trash;
using Unity.VisualScripting;
using UnityEngine;

namespace GGJ25.Game
{
    public class PowerUpController : MonoBehaviour
    {
        public enum PowerUpTypes
        {
            Multiplier,
            Bomb,
            Life,
            Freeze,
            Magnet
        }

        [Serializable]
        private struct PowerUpDetails
        {
            public string name;
            public Sprite sprite;
            public PowerUpTypes type;
        }

        [SerializeField]
        private PowerUpDetails[] powerUpDetails;

        private SpriteRenderer spriteRenderer;

        PowerUpDetails powerUp;

        private void Awake()
        {
            powerUp = powerUpDetails[UnityEngine.Random.Range(0, powerUpDetails.Length)];

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = powerUp.sprite;

            var newCollider = gameObject.AddComponent<PolygonCollider2D>();
            newCollider.isTrigger = true;
        }

        public void ActivatePower(BubbleController bubble)
        {
            var bubbleRigidbody = bubble.GetComponent<Rigidbody2D>();

            if (powerUp.type == PowerUpTypes.Multiplier)
            {
                var bubbleCollider = bubble.GetComponent<PolygonCollider2D>();

                for (int i = 0; i < 5; i++)
                {
                    var newAngle = i / 5f * (Mathf.PI * 2f);
                    var newBubble = Instantiate(bubble.gameObject, bubbleRigidbody.position + new Vector2(Mathf.Sin(newAngle), Mathf.Cos(newAngle)) * bubbleCollider.bounds.size.magnitude, Quaternion.identity);
                    var newRigidbody = newBubble.GetComponent<Rigidbody2D>();
                    newRigidbody.linearVelocity = bubbleRigidbody.linearVelocity;
                }
            }
            else if (powerUp.type == PowerUpTypes.Bomb)
            {
                foreach (var trash in TrashController.TrashControllers)
                {
                    Destroy(trash.gameObject);
                }
            }
            else if (powerUp.type == PowerUpTypes.Life)
            {
                GameManager.Singleton.GiveLife();
            }
            else if (powerUp.type == PowerUpTypes.Freeze)
            {
                foreach (var trash in TrashController.TrashControllers)
                {
                    trash.InitiateFreeze();
                }
            }
            else if (powerUp.type == PowerUpTypes.Magnet)
            {
                var newCollider = bubble.AddComponent<CircleCollider2D>();
                newCollider.isTrigger = true;
                newCollider.radius = 7;
            }
        }
    }
}
