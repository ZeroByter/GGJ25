#pragma warning disable IDE0051

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJ25.Game
{
    public class BubbleCreationController : MonoBehaviour
    {
        [SerializeField]
        private float minCreationDistance = 0.5f;
        [SerializeField]
        private GameObject bubblePrefab;
        [Range(0f, 1f)]
        [SerializeField]
        private float screenHeightRatio = 0.1f;
        [SerializeField]
        private AudioClip[] onBubbleCreatedAudio;
        [SerializeField]
        private float minCreationSize = 1f;
        [SerializeField]
        private float maxCreationSize = 6f;

        private AudioSource bubbleCreationAudioSource;
        private LineRenderer creationLineRenderer;

        private Vector3 lastMousePosition;
        private bool isDrawing;

        private List<Vector3> bubblePositions = new List<Vector3>();

        private void Awake()
        {
            creationLineRenderer = GetComponent<LineRenderer>();
            bubbleCreationAudioSource = GetComponent<AudioSource>();

            ResetLastMousePosition();
        }

        private void Update()
        {
            var bottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            var bottomRightCorner = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            var topLeftCorner = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * screenHeightRatio));

            transform.position = new Vector3(
                (bottomLeftCorner.x + bottomRightCorner.x) / 2f,
                (topLeftCorner.y + bottomLeftCorner.y) / 2f
            );

            transform.localScale = new Vector3(
                Mathf.Abs(bottomLeftCorner.x - bottomRightCorner.x),
                Mathf.Abs(topLeftCorner.y - bottomLeftCorner.y)
            );
        }

        private void ResetBubblePositions()
        {
            bubblePositions.Clear();
            creationLineRenderer.positionCount = bubblePositions.Count;
        }

        private void ResetLastMousePosition()
        {
            lastMousePosition = new Vector3(-100, -100);
        }

        private Vector3 GetCenterPosition()
        {
            var totalX = 0f;
            var totalY = 0f;

            foreach (var item in bubblePositions)
            {
                totalX += item.x;
                totalY += item.y;
            }

            return new Vector3(totalX / bubblePositions.Count, totalY / bubblePositions.Count);
        }

        private float GetPolygonArea()
        {
            var points = bubblePositions;

            if (points == null || points.Count < 3)
            {
                return 0f;
            }

            float area = 0f;
            int n = points.Count;

            for (int i = 0; i < n; i++)
            {
                Vector2 current = points[i];
                Vector2 next = points[(i + 1) % n]; // Wrap around to the first point

                area += (current.x * next.y) - (next.x * current.y);
            }

            return Mathf.Abs(area) * 0.5f;
        }

        private Vector2 GetScreenToWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void UpdateCreationLineRendererColor()
        {
            var creationSize = GetPolygonArea();
            var isCorrectSize = creationSize > minCreationSize && creationSize < maxCreationSize;

            creationLineRenderer.startColor = isCorrectSize ? Color.white : Color.red;
            creationLineRenderer.endColor = isCorrectSize ? Color.white : Color.red;
        }

        private Mesh GenerateMesh(Vector3[] localBubblePositions)
        {
            var newMesh = new Mesh();

            List<Vector3> vertices = new List<Vector3>();

            var center = Vector3.zero;

            vertices.Add(center);
            vertices.AddRange(localBubblePositions);

            List<int> triangles = new List<int>();

            int vertexCount = localBubblePositions.Length;
            for (int i = 1; i <= vertexCount; i++)
            {
                int nextIndex = (i % vertexCount) + 1;
                triangles.Add(0); // Center vertex index
                triangles.Add(i); // Current vertex index
                triangles.Add(nextIndex); // Next vertex index
            }

            newMesh.vertices = vertices.ToArray();
            newMesh.triangles = triangles.ToArray();

            float maxDistance = 0f;

            foreach (var point in localBubblePositions)
            {
                float distance = Vector2.Distance(center, point);
                maxDistance = Mathf.Max(maxDistance, distance);
            }

            newMesh.RecalculateNormals();

            return newMesh;
        }

        private void OnMouseUp()
        {
            if (isDrawing)
            {
                var newBubble = Instantiate(bubblePrefab, GetCenterPosition(), Quaternion.identity);

                var localBubblePositions = new List<Vector3>();

                foreach(var point in bubblePositions)
                {
                    localBubblePositions.Add(newBubble.transform.InverseTransformPoint(point));
                }

                var newLineRenderer = newBubble.GetComponent<LineRenderer>();
                newLineRenderer.positionCount = localBubblePositions.Count;
                newLineRenderer.SetPositions(localBubblePositions.ToArray());

                var newMesh = GenerateMesh(localBubblePositions.ToArray());

                var newMeshFilter = newBubble.GetComponent<MeshFilter>();
                newMeshFilter.sharedMesh = newMesh;

                var newPolygonCollider = newBubble.GetComponent<PolygonCollider2D>();

                var vector2BubblePositions = new Vector2[localBubblePositions.Count];
                for (int i = 0; i < localBubblePositions.Count; i++)
                {
                    vector2BubblePositions[i] = localBubblePositions[i];
                }

                newPolygonCollider.points = vector2BubblePositions;

                bubbleCreationAudioSource.clip = onBubbleCreatedAudio[Random.Range(0, onBubbleCreatedAudio.Length)];
                bubbleCreationAudioSource.Play();

                ResetBubblePositions();
                isDrawing = false;
            }
        }

        private void OnMouseDown()
        {
            isDrawing = true;
        }

        private void OnMouseDrag()
        {
            if (isDrawing)
            {
                var currentMouseWorldPosition = GetScreenToWorldPosition();

                if (Vector2.Distance(lastMousePosition, currentMouseWorldPosition) > minCreationDistance)
                {
                    lastMousePosition = currentMouseWorldPosition;
                    bubblePositions.Add(currentMouseWorldPosition);

                    creationLineRenderer.positionCount = bubblePositions.Count;
                    creationLineRenderer.SetPositions(bubblePositions.ToArray());

                    UpdateCreationLineRendererColor();
                }
            }
        }

        private void OnMouseExit()
        {
            ResetBubblePositions();
            isDrawing = false;
        }
    }
}
