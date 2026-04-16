using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private TextMeshProUGUI waveText;

    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSec = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyFactor = 0.75f;
    [SerializeField] private float enemiesPerSecCap = 15f;

    public static UnityEvent onEnemyKill = new UnityEvent();
    public static UnityEvent onDeath = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second 
    private bool isSpawning = false;
    private bool stillAlive = true;

    private void Awake()
    {
        onEnemyKill.AddListener(EnemyKilled);
        onDeath.AddListener(Die);
        onDeath.AddListener(EndWave);
    }

    private void Start()
    {
        StartCoroutine(StartWave()); //starts the process of the wave
        waveText.text = "Wave " + currentWave + "/" + LevelManager.Instance.maxWaveCount;
    }

    private void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 & enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void Die() //flips a bool so that the game stops spawning new waves
    {
        stillAlive = false;
    }
    
    private void EnemyKilled()
    {
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves); //starts up wave mechanics after time between waves passes
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0;
        if (stillAlive && currentWave < LevelManager.Instance.maxWaveCount)
        {
            currentWave++;
            waveText.text = "Wave " + currentWave + "/" + LevelManager.Instance.maxWaveCount;
            StartCoroutine(StartWave());
        }
        else if (currentWave >= LevelManager.Instance.maxWaveCount)
        {
            if (LevelManager.Instance.GetNextLevel() != -1)
            {
                SceneManager.LoadScene(LevelManager.Instance.GetNextLevel());
            }
        }
    }
    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0]; //will have more enemy types later such as ogres, trolls, and other monsters
        Instantiate(prefabToSpawn, LevelManager.Instance.startPoint.position, Quaternion.identity);
    }
    
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyFactor)); //creates scaling difficulty for enemy amount
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSec * Mathf.Pow(currentWave, difficultyFactor), 0f, enemiesPerSecCap); //creates scaling difficulty for enemy spawn times
    }
}
