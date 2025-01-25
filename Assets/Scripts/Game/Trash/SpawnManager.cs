using System;
using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        GameObject prefabToSpawn;
        [SerializeField]
        GameObject powerUpToSpawn;
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
        [SerializeField]
        private int powerUpSpawnChange = 6;

        private float lastSpawnedTime;

        private float spawnInterval;

        private void Awake()
        {
            lastSpawnedTime = -10;

            GameManager.OnScoreChanged += HandleScoreChanged;

            HandleScoreChanged(0);
        }

        void Update()
        {
            if (autoSpawn && Time.time > lastSpawnedTime + spawnInterval)
            {
                lastSpawnedTime = Time.time;
                SpawnObject();

                if(UnityEngine.Random.Range(0, powerUpSpawnChange) == 0)
                {
                    SpawnPowerUp();
                }
            }
        }

        private void HandleScoreChanged(int newScore)
        {
            spawnInterval = Mathf.Lerp(initialSpawnInterval, fastestSpawnInterval, (float)newScore / maxScoreForIntervalAdjustment);
        }

        private Vector3 GetSpawnPosition()
        {
            var randomDirection = UnityEngine.Random.Range(0, 2) == 0;
            Vector2 worldPosition;

            var randomHeight = UnityEngine.Random.Range(minYScreen, maxYScreen);

            if (randomDirection)
            {
                worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(-0.1f, randomHeight));
            }
            else
            {
                worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1.1f, randomHeight));
            }

            return new Vector3(worldPosition.x, worldPosition.y, -0.1f);
        }

        void SpawnObject()
        {
            if (prefabToSpawn == null) { return; }

            Vector2 spawnWorldPos = GetSpawnPosition();
            var newTrash = Instantiate(prefabToSpawn, spawnWorldPos, Quaternion.identity);
            if (spawnWorldPos.x < 0)
            {
                newTrash.GetComponent<TrashMovement>().Setup(new Vector2(1, 0));
            }
            else
            {
                newTrash.GetComponent<TrashMovement>().Setup(new Vector2(-1, 0));
            }
        }

        void SpawnPowerUp()
        {
            if (powerUpToSpawn == null) { return; }

            Vector2 spawnWorldPos = GetSpawnPosition();
            var newPowerUp = Instantiate(powerUpToSpawn, spawnWorldPos, Quaternion.identity);
            if (spawnWorldPos.x < 0)
            {
                newPowerUp.GetComponent<TrashMovement>().Setup(new Vector2(1, 0));
            }
            else
            {
                newPowerUp.GetComponent<TrashMovement>().Setup(new Vector2(-1, 0));
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