using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public float spawnCooldown = 2f;

    [Header("Границы карты")]
    public float minX = -15f;
    public float maxX = 15f;
    public float minZ = -15f;
    public float maxZ = 15f;

    private float nextSpawnTime;

    void Update()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemies < maxEnemies && Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnCooldown;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 randomPos = new Vector3(randomX, 1f, randomZ);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (Vector3.Distance(randomPos, player.transform.position) < 5f)
            {
                return;
            }
        }

        Instantiate(enemyPrefab, randomPos, Quaternion.identity);
    }
}