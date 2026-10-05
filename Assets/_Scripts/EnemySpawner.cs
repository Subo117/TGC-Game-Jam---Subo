using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject easyEnemyPrefab;
    [SerializeField] private GameObject mediumEnemyPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [SerializeField] private float startSpawnTime = 2f;
    [SerializeField] private float minimumSpawnTime = 0.5f;

    private float spawnTimer;
    private float gameTime;

    private void OnEnable()
    {
        CentreArea.OnFinalMoment += StopSpawning;
    }

    private void OnDisable()
    {
        CentreArea.OnFinalMoment -= StopSpawning;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = GetSpawnTime();
        }
    }

    float GetSpawnTime()
    {
        float difficulty = Mathf.Clamp01(gameTime / 300f);

        return Mathf.Lerp(startSpawnTime, minimumSpawnTime, difficulty);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        float mediumChance = GetMediumChance();

        GameObject enemyToSpawn;

        if (Random.Range(0f, 100f) < mediumChance)
        {
            enemyToSpawn = mediumEnemyPrefab;
        }
        else
        {
            enemyToSpawn = easyEnemyPrefab;
        }

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    private void StopSpawning()
    {
        enabled = false;
    }

    float GetMediumChance()
    {
        float minutes = gameTime / 60f;

        if (minutes < 1f)
            return 0f;

        if (minutes < 2f)
            return 10f;

        if (minutes < 3f)
            return 25f;

        if (minutes < 4f)
            return 40f;

        if (minutes < 5f)
            return 60f;

        return 80f;
    }
}
