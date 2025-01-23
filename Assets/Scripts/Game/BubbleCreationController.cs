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
        [SerializeField]
        private float screenHeightRatio = 0.1f;

        private LineRenderer creationLineRenderer;

        private Vector3 lastMousePosition;
        private bool isDrawing;

        private List<Vector3> bubblePositions = new List<Vector3>();

        private void Awake()
        {
            creationLineRenderer = GetComponent<LineRenderer>();

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

        private Vector2 GetScreenToWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private Mesh GenerateMesh()
        {
            var newMesh = new Mesh();

            List<Vector3> vertices = new List<Vector3>();

            var center = GetCenterPosition();

            vertices.Add(center);
            vertices.AddRange(bubblePositions);

            List<int> triangles = new List<int>();

            int vertexCount = bubblePositions.Count;
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

            foreach (var point in bubblePositions)
            {
                float distance = Vector2.Distance(center, point);
                maxDistance = Mathf.Max(maxDistance, distance);
            }

            newMesh.RecalculateNormals();

            return newMesh;
        }

        private void OnMouseUp()
        {
            var newBubble = Instantiate(bubblePrefab);

            var newLineRenderer = newBubble.GetComponent<LineRenderer>();
            newLineRenderer.positionCount = bubblePositions.Count;
            newLineRenderer.SetPositions(bubblePositions.ToArray());

            var newMeshFilter = newBubble.GetComponent<MeshFilter>();
            newMeshFilter.sharedMesh = GenerateMesh();

            ResetBubblePositions();
            isDrawing = false;
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
