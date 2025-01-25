using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace GGJ25.Game
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer cursor;
        [SerializeField]
        private Bounds tutorialBounds;
        [SerializeField]
        private float minDistanceBetweenPoints = 0.1f;
        [SerializeField]
        private int maxLinePoints = 20;
        [SerializeField]
        private float speed;

        [SerializeField]
        private Sprite[] icons;

        private LineRenderer lineRenderer;

        private List<Vector3> linePoints = new List<Vector3>();

        private bool showTutorial = true;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();

            BubbleCreationController.BubbleCreated += HandleBubbleCreated;
        }

        private void HandleBubbleCreated()
        {
            gameObject.SetActive(false);
        }

        private Vector3 GetLastLinePoint()
        {
            if(linePoints.Count == 0)
            {
                return new Vector3(0, 0, 0);
            }

            return linePoints[linePoints.Count - 1];
        }

        private void Update()
        {
            if (!showTutorial)
            {
                return;
            }

            var time = Time.unscaledTime * speed;

            cursor.sprite = icons[Mathf.FloorToInt(time % icons.Length)];

            var lastPoint = GetLastLinePoint();
            var newPoint = new Vector3(Mathf.Sin(time) * tutorialBounds.extents.x + tutorialBounds.center.x, Mathf.Cos(time) * tutorialBounds.extents.y + tutorialBounds.center.y);

            cursor.transform.position = newPoint;

            if (Vector3.Distance(lastPoint, newPoint) > minDistanceBetweenPoints)
            {
                linePoints.Add(newPoint);

                if(linePoints.Count > maxLinePoints)
                {
                    linePoints.RemoveAt(0);
                }

                lineRenderer.positionCount = linePoints.Count;
                lineRenderer.SetPositions(linePoints.ToArray());
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(tutorialBounds.center, tutorialBounds.size);
        }

        private void OnDestroy()
        {
            BubbleCreationController.BubbleCreated -= HandleBubbleCreated;
        }
    }
}
