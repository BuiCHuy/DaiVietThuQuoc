using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float enemiesPerSecond = 1;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float DifficcultyMultiplier = 0.75f;
    private int currentWave = 1;
    private float timeSinceLastSpawn = 0f;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public static UnityEvent onEnemyDestroyed = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartWave());
    }
    void Awake()
    {
        onEnemyDestroyed.AddListener(EnemyDestroyed);   
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f/ enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if(enemiesAlive ==0 && enemiesLeftToSpawn ==0)
        {
            EndWave();
        }
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }
    void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyPrefabs[0];
        Instantiate(enemyToSpawn,LevelManager.main.start.position,Quaternion.identity);
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies*Mathf.Pow(currentWave,DifficcultyMultiplier));
    }
    void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    void EndWave()
    {
        isSpawning = false;
        currentWave++;
        timeSinceLastSpawn = 0;
        StartCoroutine(StartWave());
    }
}
