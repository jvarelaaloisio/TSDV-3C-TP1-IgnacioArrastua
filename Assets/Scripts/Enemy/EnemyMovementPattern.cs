using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Class for the EnemyMovementPattern
/// </summary>
public class EnemyMovementPattern : MonoBehaviour
{
    [SerializeField] private EnemyMovement[] enemyTypes;
    [SerializeField] private Transform enemyHolder;
    [SerializeField] private int defaultEnemyCount = 0;
    [SerializeField] private int specialEnemyCount = 0;
    private List<EnemyMovement> enemysToSpawn;
    [SerializeField]
    private Transform bulletHolder;
    private bool isActive = false;
    [SerializeField] private Color color;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    [SerializeField] private bool shouldLoop = false;
    [SerializeField] private float loopTimes;
    [SerializeField] private int startLoop;
    [SerializeField] private int endLoop;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private int enemyCounter;
    private int maxEnemysInScene;
    private float spawnTimer = 0.0f;

    private void OnValidate()
    {
        points = transform.Cast<Transform>().ToArray();

    }
    private void Start()
    {
        if (isActive)
            StartPattern();
    }
    /// <summary>
    /// Initialize enemy Pattern
    /// </summary>
    public void StartPattern()
    {
        endLoop = Mathf.Clamp(endLoop, startLoop, points.Length - 1);
        startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);
        SetMaxEnemiesInScreen();
        SetRandomEnemies();
        maxEnemysInScene = enemysToSpawn.Count;
        isActive = true;
    }
    /// <summary>
    /// Randomize enemies from list
    /// </summary>
    private void SetRandomEnemies()
    {
        for (int i = 0; i < enemysToSpawn.Count; i++)
        {
            var temp = enemysToSpawn[i];
            int randomIndex = Random.Range(i, enemysToSpawn.Count);
            enemysToSpawn[i] = enemysToSpawn[randomIndex];
            enemysToSpawn[randomIndex] = temp;
        }
    }
    /// <summary>
    /// Creates enemies according to type and sets their properties
    /// </summary>
    private void SetMaxEnemiesInScreen()
    {
        enemyCounter = 0;
        enemysToSpawn = new List<EnemyMovement>();

        for (int i = 0; i < defaultEnemyCount; i++)
        {
            var aux = Instantiate(enemyTypes[0], enemyHolder);
            enemysToSpawn.Add(aux);
        }

        for (int i = 0; i < specialEnemyCount; i++)
        {
            var aux = Instantiate(enemyTypes[1], enemyHolder);
            enemysToSpawn.Add(aux);
        }

        foreach (var enemy in enemysToSpawn)
        {
            enemy.SetStartParameters(speed, shouldLoop, loopTimes, startLoop, endLoop, false, points);
        }
    }

    private void Update()
    {
        SpawnEnemies();
    }
    /// <summary>
    /// Activate Enemies in scene according to spawnTimer and enemy count
    /// </summary>
    private void SpawnEnemies()
    {
        if (isActive)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay && enemyCounter < maxEnemysInScene)
            {
                spawnTimer = 0.0f;
                enemysToSpawn[enemyCounter].SetActive();
                enemyCounter++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.1f);
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
}
