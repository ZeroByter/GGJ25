using UnityEngine;

namespace GGJ25.Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteInEditMode]
    public class LerpCanvasGroup : MonoBehaviour
    {
        [Range(0, 1)]
        public float target;
        public float lerp = 50;
        public bool toggleBlockRaycasts = false;
        public float setToZeroThreshold = 0;

        public float currentAlpha
        {
            get
            {
                return cGroup.alpha;
            }
        }

        [HideInInspector]
        public CanvasGroup cGroup { get; private set; }

        private void Awake()
        {
            cGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            cGroup.alpha = Mathf.Lerp(cGroup.alpha, target, lerp * Time.unscaledDeltaTime);
            if (toggleBlockRaycasts)
            {
                cGroup.blocksRaycasts = cGroup.alpha > 0.05f;
            }

            if (cGroup.alpha < setToZeroThreshold) cGroup.alpha = 0;
        }

        public void SetAlpha(float alpha, bool force = false)
        {
            if (cGroup == null) return;

            target = alpha;
            if (force)
            {
                cGroup.alpha = alpha;
            }
        }
    }

}
