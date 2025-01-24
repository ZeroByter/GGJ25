using UnityEngine;

namespace GGJ25.Game.UI
{
    public class HeartsManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private GameObject heartTemplate;

        private void Awake()
        {
            heartTemplate.SetActive(false);

            GameManager.OnLivesChanged += UpdateHearts;

            UpdateHearts(gameManager.lives);
        }

        private void UpdateHearts(int newLives)
        {
            foreach (Transform existingHeart in heartTemplate.transform.parent)
            {
                if (existingHeart.gameObject.activeSelf)
                {
                    Destroy(existingHeart.gameObject);
                }
            }

            for(int i = 0; i < newLives; i++)
            {
                var newHeart = Instantiate(heartTemplate, heartTemplate.transform.parent);
                newHeart.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            GameManager.OnLivesChanged -= UpdateHearts;
        }
    }
}
