using UnityEngine;

namespace GGJ25.MainMenu {
    public class MainMenuSpritesSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private int spritesToSpawn;
        [SerializeField]
        private float spawnCooldown = 0.5f;
        [SerializeField]
        private Bounds spawnBounds;

        private int spawnedSprites;
        private float lastSpawnedSprite;

        private void Update()
        {
            if(spawnedSprites < spritesToSpawn && Time.time > lastSpawnedSprite + spawnCooldown)
            {
                lastSpawnedSprite = Time.time;
                spawnedSprites++;

                var newTrash = Instantiate(
                    prefab,
                    new Vector3(
                        spawnBounds.center.x + Random.Range(
                            -spawnBounds.extents.x,
                            spawnBounds.extents.x
                        ), 
                        spawnBounds.center.y + Random.Range(
                            -spawnBounds.extents.y,
                            spawnBounds.extents.y
                        ),
                        0
                    ),
                    Quaternion.Euler(0, 0, Random.Range(0, 360f))
                );
                newTrash.AddComponent<DestroyDiscardedTrash>();
            }
        }

        public void StopSpawningSprites()
        {
            spawnedSprites = spritesToSpawn;
        }

        public void ResetSpawnedSprites()
        {
            spawnedSprites = 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(spawnBounds.center, spawnBounds.size);
        }
    }
}
