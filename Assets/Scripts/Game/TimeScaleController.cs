using UnityEngine;

namespace GGJ25.Game
{
    public class TimeScaleController : MonoBehaviour
    {
        [SerializeField]
        private float lerpSpeed = 30f;

        private float targetTimeScale = 1f;

        private void Awake()
        {
            GameManager.OnGameRunningChanged += HandleGameRunningChange;
        }

        private void Update()
        {
            var newTimeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, lerpSpeed * Time.unscaledDeltaTime);

            if (newTimeScale < 0.01f)
            {
                newTimeScale = 0f;
            }
            if (newTimeScale > 1f - 0.01f)
            {
                newTimeScale = 1f;
            }

            Time.timeScale = newTimeScale;
        }

        private void HandleGameRunningChange(bool isGameRunning)
        {
            targetTimeScale = isGameRunning ? 1f : 0f;
        }

        private void OnDestroy()
        {
            GameManager.OnGameRunningChanged -= HandleGameRunningChange;
        }
    }
}
