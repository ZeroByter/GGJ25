using TMPro;
using UnityEngine;

namespace GGJ25.Game.UI
{
    public class AvailableBubblesController : MonoBehaviour
    {
        private TMP_Text scoreText;

        private void Awake()
        {
            scoreText = GetComponent<TMP_Text>();

            GameManager.OnAvailableBubblesChanged += HandleBubblesChanged;

            HandleBubblesChanged(GameManager.Singleton.bubblesAvailable);
        }

        private void HandleBubblesChanged(int newCount)
        {
            scoreText.text = $"Bubbles: {newCount}/{GameManager.Singleton.maxBubbles}";
        }

        private void OnDestroy()
        {
            GameManager.OnAvailableBubblesChanged -= HandleBubblesChanged;
        }
    }
}
