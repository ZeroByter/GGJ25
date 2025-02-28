using GGJ25.Game;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ25
{
    public class TrashFailureFeedback : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed = 50f;
        [Range(0f, 1f)]
        [SerializeField]
        private float flashAlpha = 0.2509804f;

        Image feedbackImage;
        private Color originalColor;

        void Awake()
        {
            feedbackImage = GetComponent<Image>();

            originalColor = feedbackImage.color;
            SetImageAlpha(0f);
        }

        private void SetImageAlpha(float alpha){
            feedbackImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }

        private void Update()
        {
            SetImageAlpha(Mathf.Lerp(feedbackImage.color.a, 0f, fadeSpeed * Time.unscaledDeltaTime));
        }

        void OnEnable()
        {
            GameManager.OnTrashFailureChanged += ShowRedFlash;
        }

        void OnDisable()
        {
            GameManager.OnTrashFailureChanged -= ShowRedFlash;
        }

        private void ShowRedFlash()
        {
            SetImageAlpha(flashAlpha);
        }
    }
}