using GGJ25.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ25 {
    public class BombEffect : MonoBehaviour
    {
        public static BombEffect Singleton { get; private set; }

        [SerializeField]
        private float fadeSpeed = 50f;
        [Range(0f, 1f)]
        [SerializeField]
        private float flashAlpha = 0.22f;

        Image feedbackImage;
        private Color originalColor;

        void Awake()
        {
            Singleton = this;

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

        public void ShowFlash()
        {
            SetImageAlpha(flashAlpha);
        }
    }
}