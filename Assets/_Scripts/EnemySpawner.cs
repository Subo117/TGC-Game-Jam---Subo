using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject easyEnemyPrefab;
    [SerializeField] private GameObject mediumEnemyPrefab;

    [Header("Normal Spawning")]
    [SerializeField] private float startSpawnTime = 2f;
    [SerializeField] private float minimumSpawnTime = 0.5f;

    [Header("Arena")]
    [SerializeField] private float minX = -18f;
    [SerializeField] private float maxX = 18f;
    [SerializeField] private float minY = -10f;
    [SerializeField] private float maxY = 10f;

    [Header("Final Moment")]
    [SerializeField] private int minFinalEnemies = 10;
    [SerializeField] private int maxFinalEnemies = 15;

    [Header("Camera View")]
    [SerializeField] private float cameraMinX = -13f;
    [SerializeField] private float cameraMaxX = 13f;
    [SerializeField] private float cameraMinY = -7f;
    [SerializeField] private float cameraMaxY = 7f;

    private float spawnTimer;
    private float gameTime;

    private void Awake()
    {
        spawnTimer = 0f;
        gameTime = 0f;
    }

    private void OnEnable()
    {
        CentreArea.OnFinalMoment += StopSpawning;
    }

    private void OnDisable()
    {
        CentreArea.OnFinalMoment -= StopSpawning;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = GetSpawnTime();
        }
    }

    private float GetSpawnTime()
    {
        float difficulty = Mathf.Clamp01(gameTime / 300f);

        return Mathf.Lerp(startSpawnTime, minimumSpawnTime, difficulty);
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        GameObject enemyToSpawn = GetEnemyPrefab();

        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private void StopSpawning()
    {
        int enemyCount = Random.Range(minFinalEnemies, maxFinalEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemyOutsideCamera();
        }

        enabled = false;
    }

    private void SpawnEnemyOutsideCamera()
    {
        Vector2 spawnPosition;

        int attempts = 0;

        do
        {
            spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            attempts++;

        } while (
            spawnPosition.x > cameraMinX &&
            spawnPosition.x < cameraMaxX &&
            spawnPosition.y > cameraMinY &&
            spawnPosition.y < cameraMaxY &&
            attempts < 100
        );

        GameObject enemyToSpawn = GetEnemyPrefab();

        if (enemyToSpawn != null)
        {
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private GameObject GetEnemyPrefab()
    {
        float mediumChance = GetMediumChance();

        if (Random.Range(0f, 100f) < mediumChance)
        {
            return mediumEnemyPrefab;
        }

        return easyEnemyPrefab;
    }

    private float GetMediumChance()
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