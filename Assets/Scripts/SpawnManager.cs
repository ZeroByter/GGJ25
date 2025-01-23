using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float spawnInterval = 1f;
    private float nextSpawnTime;
    [SerializeField] bool autoSpawn = true;
    [SerializeField] private float minYWorld = -3f;
    [SerializeField] private float maxYWorld = 2f;

    void Start()
    {

    }

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
        float xWorld;
        bool randomDirection;
        if (randomSideOfScreen == 0)
        {
            xWorld = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
            randomDirection = true;
        }
        else
        {
            xWorld = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
            randomDirection = false;
        }
        float yWorld = Random.Range(minYWorld, maxYWorld);

        Vector2 spawnWorldPos = new Vector2(xWorld, yWorld);
        var newTrash = Instantiate(prefabToSpawn, spawnWorldPos, Quaternion.identity);
        if (randomDirection)
        {
            newTrash.GetComponent<TrashMovement>().Setup(new Vector2(-1, 0));
        }
        else
        {
            newTrash.GetComponent<TrashMovement>().Setup(new Vector2(1, 0));
        }
    }
}