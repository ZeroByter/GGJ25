using System.Collections.Generic;
using UnityEngine;

namespace GGJ25.Game
{
    public class BubbleController : MonoBehaviour
    {
        [SerializeField]
        private PlayRandomAudio powerUpRandomAudio;
        
        private new Rigidbody2D rigidbody;

        private List<GameObject> trashObjects = new List<GameObject>();

        public void Setup(float speed)
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.linearVelocity = new Vector3(0, speed);
        }

        private void Update()
        {
            if (transform.position.y > 18f)
            {
                foreach (var trash in trashObjects)
                {
                    Destroy(trash);
                }

                if(trashObjects.Count == 1)
                {
                    GameManager.Singleton.AddScore(trashObjects.Count);
                }
                else
                {
                    GameManager.Singleton.AddScore(trashObjects.Count * 2);
                }

                Destroy(gameObject);
            }
        }

        public void AddCaughtTrash(GameObject trashObject)
        {
            trashObjects.Add(trashObject);
        }

        public void PlayRandomPowerUpAudio()
        {
            powerUpRandomAudio.Play();
        }
    }
}
