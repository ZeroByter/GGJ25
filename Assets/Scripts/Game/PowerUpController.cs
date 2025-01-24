using System;
using System.Linq;
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
            public float weight;
        }

        [SerializeField]
        private PowerUpDetails[] powerUpDetails;

        private SpriteRenderer spriteRenderer;

        PowerUpDetails powerUp;

        private void Awake()
        {
            powerUp = GetRandomPowerUp();
            //powerUp = powerUpDetails[4];

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = powerUp.sprite;

            var newCollider = gameObject.AddComponent<PolygonCollider2D>();
            newCollider.isTrigger = true;
        }

        private PowerUpDetails GetRandomPowerUp()
        {
            var totalWeight = powerUpDetails.Sum(i => i.weight);
            var randomValue = UnityEngine.Random.Range(0, totalWeight);

            float sum = 0f;
            foreach (var powerUp in powerUpDetails)
            {
                sum += powerUp.weight;
                if(randomValue < sum)
                {
                    return powerUp;
                }
            }

            return powerUpDetails[UnityEngine.Random.Range(0, powerUpDetails.Length)];
        }

        public void ActivatePower(BubbleController bubble)
        {
            var bubbleRigidbody = bubble.GetComponent<Rigidbody2D>();
            var bubbleCollider = bubble.GetComponent<PolygonCollider2D>();

            if (powerUp.type == PowerUpTypes.Multiplier)
            {
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
                BombEffect.Singleton.ShowFlash();
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
                newCollider.radius = bubbleCollider.bounds.extents.magnitude * 2.5f;
            }
        }
    }
}
