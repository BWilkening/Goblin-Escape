using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] instructionPrefabs;
    [SerializeField] private GameObject[] obstaclePrefabs;

    [SerializeField] private GameObject[] projectilePrefabs;

    [SerializeField] private GameObject[] foodPrefabs;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private GameObject SpiderPrefab;


    [SerializeField] public Transform objectParent;

    #region Obstaclefactors
    float difficultySpeed => SceneController.instance.universalSpeed; // Using the universal speed from SceneController
    public float obstacleSpawnTime = 3f;
    [Range(0,1)] public float obstacleSpawnTimeFactor = 0.1f;
    public float obstacleSpeed = 4f;
    [Range(0,1)] public float obstacleSpeedFactor = 0.2f;

    private float _obstacleSpawnTime;
    public float _obstacleSpeed;

    private float timeUntilObstacleSpawn;
    #endregion
    
    #region Projectilefactors
    [Header("Projectilefactors")]

    public float projectileSpawnTime = 6f;
    [Range(0,1)] public float projectileSpawnTimeFactor = 0.1f;
    public float projectileSpeed = 8f;
    [Range(0,1)] public float projectileSpeedFactor = 0.2f;

    private float _projectileSpawnTime;
    public float _projectileSpeed;

    private float timeUntilProjectileSpawn;
    #endregion

    #region Foodfactors
    [Header("Foodfactors")]
    public float foodSpawnTime = 20f;
    [Range(0,1)] public float foodSpawnTimeFactor = 0.1f;
    public float foodSpeed = 4f;
    [Range(0,1)] public float foodSpeedFactor = 0.2f;

    private float _foodSpawnTime;
    public float _foodSpeed;

    private float timeUntilFoodSpawn;
    #endregion

    #region Enemyfactors
[Header("Enemyfactors")]
    public float enemySpawnTime = 3f;
    [Range(0,1)] public float enemySpawnTimeFactor = 0.1f;
    public float enemySpeed = 4f;
    [Range(0,1)] public float enemySpeedFactor = 0.2f;

    private float _enemySpawnTime;
    public float _enemySpeed;

    private float timeUntilEnemySpawn;
    #endregion

    #region Bossfactors
    [Header("Bossfactors")]

    public float bossSpawnTime = 30f;
    public bool BossCanSpawn = true;
    private float timeUntilBossSpawn;
    public int Level = 1;
    public bool spiderBossIsKilled = false;

    public GameObject level1;
    public GameObject level2;

    #endregion

    private bool StartInstructions = true;
    private  float spawnBreak = 0.5f;
    private float spawnBreakTimer = 0f;
    public float timeAlive;

    private void Start() {
        GameManager.Instance.onGameOver.AddListener(ClearObjects);
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }

    private void Update() {
        if (GameManager.Instance.isPlaying) {
            timeAlive += Time.deltaTime;
            ShowInstructions();

            CalculateFactors();

            SpawnLoop();

            if (spiderBossIsKilled == true)
            {
                Level = 2;
                level1.SetActive(false);
                level2.SetActive(true);
            }
        }
    }

    private void ShowInstructions() {
        if (StartInstructions == true) {
            GameObject InstructiontoSpawn = instructionPrefabs[0];
        
        Vector3 posToSpawn = new Vector3(-6, 2, 0);
        GameObject spawnedInstruction = Instantiate(InstructiontoSpawn, posToSpawn, Quaternion.identity);

        InstructiontoSpawn = instructionPrefabs[1];
        
        posToSpawn = new Vector3(0, -5, 0);
        spawnedInstruction = Instantiate(InstructiontoSpawn, posToSpawn, Quaternion.identity);

        InstructiontoSpawn = instructionPrefabs[2];
        
        posToSpawn = new Vector3(8, 2, 0);
        spawnedInstruction = Instantiate(InstructiontoSpawn, posToSpawn, Quaternion.identity);

        InstructiontoSpawn = instructionPrefabs[3];
        
        posToSpawn = new Vector3(15, -5, 0);
        spawnedInstruction = Instantiate(InstructiontoSpawn, posToSpawn, Quaternion.identity);

        InstructiontoSpawn = instructionPrefabs[4];
        
        posToSpawn = new Vector3(20, 2, 0);
        spawnedInstruction = Instantiate(InstructiontoSpawn, posToSpawn, Quaternion.identity);

        StartInstructions = false;
        }
    }

    private void SpawnLoop() {
        spawnBreakTimer += Time.deltaTime;

        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= _obstacleSpawnTime && spawnBreakTimer > spawnBreak) {
            SpawnObstacle();
            timeUntilObstacleSpawn = 0f;
            spawnBreakTimer = 0f;

        }

        timeUntilProjectileSpawn += Time.deltaTime;

        if (timeUntilProjectileSpawn >= _projectileSpawnTime && spawnBreakTimer > spawnBreak) {
            SpawnProjectile();
            timeUntilProjectileSpawn = 0f;
            spawnBreakTimer = 0f;
        }

        timeUntilFoodSpawn += Time.deltaTime;

        if (timeUntilFoodSpawn >= _foodSpawnTime && spawnBreakTimer > spawnBreak) {
            SpawnFood();
            timeUntilFoodSpawn = 0f;
            spawnBreakTimer = 0f;
        }

        timeUntilEnemySpawn += Time.deltaTime;

        if (timeUntilEnemySpawn >= _enemySpawnTime && spawnBreakTimer > spawnBreak) {
            SpawnEnemy();
            timeUntilEnemySpawn = 0f;
            spawnBreakTimer = 0f;
        }
        if (BossCanSpawn == true) {
            timeUntilBossSpawn += Time.deltaTime;

            if(timeUntilBossSpawn >= bossSpawnTime)
            {
                SpawnBoss();
                BossCanSpawn = false;
                timeUntilBossSpawn = 0f;
            }
        }
    }

    private void ClearObjects() {
        foreach (Transform child in objectParent) {
            Destroy(child.gameObject);
        }
    }

    private void CalculateFactors() {
        // Adjust spawn time and speed based on universal speed modifier
        _obstacleSpawnTime = (obstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor)) / difficultySpeed;
        _obstacleSpeed = (obstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor)) * difficultySpeed;

        _projectileSpawnTime = (projectileSpawnTime / Mathf.Pow(timeAlive, projectileSpawnTimeFactor)) / difficultySpeed;
        _projectileSpeed = (projectileSpeed * Mathf.Pow(timeAlive, projectileSpeedFactor)) * difficultySpeed;

        _foodSpawnTime = (foodSpawnTime / Mathf.Pow(timeAlive, foodSpawnTimeFactor)) / difficultySpeed;
        _foodSpeed = (foodSpeed * Mathf.Pow(timeAlive, foodSpeedFactor)) * difficultySpeed;
        
        _enemySpawnTime = (enemySpawnTime / Mathf.Pow(timeAlive, enemySpawnTimeFactor)) / difficultySpeed;
        _enemySpeed = (enemySpeed * Mathf.Pow(timeAlive, enemySpeedFactor)) * difficultySpeed;
    }

    private void ResetFactors() {
        timeAlive = 1f;
        _obstacleSpawnTime = obstacleSpawnTime;
        _obstacleSpeed = obstacleSpeed;
        _projectileSpawnTime = projectileSpawnTime;
        _projectileSpeed = projectileSpeed;
        _foodSpawnTime = foodSpawnTime;
        _foodSpeed = foodSpeed;
        _enemySpawnTime = enemySpawnTime;
        _enemySpeed = enemySpeed;
        BossCanSpawn = true;
        spiderBossIsKilled = false;
        StartInstructions = true;
        Level = 1;
        level1.SetActive(true);
        level2.SetActive(false);
    }

    private void SpawnObstacle() {
        if (Level == 1 && timeAlive < 50) {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, 3)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = objectParent;
        }

        if (Level == 1 && timeAlive >= 50) {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, 5)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = objectParent;
        }

        if (Level == 2) {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(6, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = objectParent;
        }
    }

    private void SpawnProjectile() {
        if (Level == 1){
        GameObject projectileToSpawn = projectilePrefabs[Random.Range(0, 1)];

        GameObject spawnedProjectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        spawnedProjectile.transform.parent = objectParent;
        }

        if (Level == 2 && timeAlive < 300){
        GameObject projectileToSpawn = projectilePrefabs[Random.Range(2, 3)];

        GameObject spawnedProjectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        spawnedProjectile.transform.parent = objectParent;
        }

        if (Level == 2 && timeAlive >= 300){
        GameObject projectileToSpawn = projectilePrefabs[Random.Range(2, projectilePrefabs.Length)];

        GameObject spawnedProjectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        spawnedProjectile.transform.parent = objectParent;
        }
    }

    private void SpawnFood() {
        GameObject foodToSpawn = foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        GameObject spawnedFood = Instantiate(foodToSpawn, transform.position, Quaternion.identity);
        spawnedFood.transform.parent = objectParent;
    }

    private void SpawnEnemy() {
        if(Level == 1 && timeAlive < 50){
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, 1)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = objectParent;
        }

        if(Level == 1 && timeAlive >= 50){
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, 2)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = objectParent;
        }

        if(Level == 2){
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(3, enemyPrefabs.Length)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = objectParent;
        }
    }

    private void SpawnBoss()
    {
        GameObject bossToSpawn = SpiderPrefab;
        
        Vector3 posToSpawn = new Vector3(9, 0, 0);
        GameObject spawnedBoss = Instantiate(bossToSpawn, posToSpawn, Quaternion.identity);
        spawnedBoss.transform.parent = objectParent;

        Rigidbody2D bossRB = spawnedBoss.GetComponent<Rigidbody2D>();
    }

}
