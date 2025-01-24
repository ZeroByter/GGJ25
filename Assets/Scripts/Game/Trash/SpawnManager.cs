using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        GameObject prefabToSpawn;
        [SerializeField]
        private bool autoSpawn = true;

        [SerializeField]
        private float initialSpawnInterval = 3f;
        [SerializeField]
        private float fastestSpawnInterval = 0.9f;
        [SerializeField]
        private int maxScoreForIntervalAdjustment = 20;

        [Range(0f, 1f)]
        [SerializeField]
        private float minYScreen = 0f;
        [Range(0f, 1f)]
        [SerializeField]
        private float maxYScreen = 1f;

        private float lastSpawnedTime;

        private float spawnInterval;

        private void Awake()
        {
            GameManager.OnScoreChanged += HandleScoreChanged;

            HandleScoreChanged(0);
        }

        void Update()
        {
            if (autoSpawn && Time.time > lastSpawnedTime + spawnInterval)
            {
                lastSpawnedTime = Time.time;
                SpawnObject();
            }
        }

        private void HandleScoreChanged(int newScore)
        {
            spawnInterval = Mathf.Lerp(initialSpawnInterval, fastestSpawnInterval, (float)newScore / maxScoreForIntervalAdjustment);
        }

        void SpawnObject()
        {
            if (prefabToSpawn == null) { return; }
            int randomSideOfScreen = UnityEngine.Random.Range(0, 2);
            Vector2 worldPosition;
            bool randomDirection;

            var randomHeight = UnityEngine.Random.Range(minYScreen, maxYScreen);

            if (randomSideOfScreen == 0)
            {
                worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, randomHeight));
                randomDirection = true;
            }
            else
            {
                worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, randomHeight));
                randomDirection = false;
            }

            Vector2 spawnWorldPos = new Vector2(worldPosition.x, worldPosition.y);
            var newTrash = Instantiate(prefabToSpawn, spawnWorldPos, Quaternion.identity);
            if (randomDirection)
            {
                newTrash.GetComponent<TrashMovement>().Setup(new Vector2(1, 0));
            }
            else
            {
                newTrash.GetComponent<TrashMovement>().Setup(new Vector2(-1, 0));
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var minWorldPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, minYScreen));
            var maxWorldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, maxYScreen));

            Gizmos.DrawWireCube((minWorldPosition + maxWorldPosition) / 2f, new Vector3(maxWorldPosition.x - minWorldPosition.x, maxWorldPosition.y - minWorldPosition.y, 1f));
        }
#endif

        private void OnDestroy()
        {
            GameManager.OnScoreChanged -= HandleScoreChanged;
        }
    }
}