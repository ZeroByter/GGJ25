using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject prefabToSpawn;
        [SerializeField] float spawnInterval = 1f;
        private float nextSpawnTime;
        [SerializeField] bool autoSpawn = true;
        [Range(0f, 1f)]
        [SerializeField]
        private float minYScreen = 0f;
        [Range(0f, 1f)]
        [SerializeField]
        private float maxYScreen = 1f;

        void Update()
        {
            if (!autoSpawn) { return; }

            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime = Time.time + spawnInterval;
                SpawnObject();
            }
        }

        void SpawnObject()
        {
            if (prefabToSpawn == null) { return; }
            int randomSideOfScreen = Random.Range(0, 2);
            Vector2 worldPosition;
            bool randomDirection;

            var randomHeight = Random.Range(minYScreen, maxYScreen);

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
    }
}