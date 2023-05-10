using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovementPattern : MonoBehaviour
{
    [SerializeField] private EnemyMovement[] enemyTypes;
    [SerializeField] private int defaultEnemyCount = 0;
    [SerializeField] private int specialEnemyCount = 0;
    private List<EnemyMovement> enemysToSpawn;
    [SerializeField]
    private Transform bulletHolder;
    private bool isActive = false;

    [SerializeField] private float speed;
    [SerializeField] Transform[] points;
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
    void Start()
    {
        if (isActive)
            StartPattern();
    }

    public void StartPattern()
    {
        enemyCounter = 0;
        enemysToSpawn = new List<EnemyMovement>();
        endLoop = Mathf.Clamp(endLoop, startLoop, points.Length - 1);
        startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);

        for (int i = 0; i < defaultEnemyCount; i++)
        {
            var aux = Instantiate(enemyTypes[0]);
            enemysToSpawn.Add(aux);
        }
        for (int i = 0; i < specialEnemyCount; i++)
        {
            var aux = Instantiate(enemyTypes[1]);
            enemysToSpawn.Add(aux);
        }

        foreach (var enemy in enemysToSpawn)
        {
            enemy.SetStartParameters(speed, shouldLoop, loopTimes, startLoop, endLoop, false, points);
            enemy.gameObject.GetComponent<EnemyShooting>().SetBulletHolder(bulletHolder);
        }
        for (int i = 0; i < enemysToSpawn.Count; i++)
        {
            var temp = enemysToSpawn[i];
            int randomIndex = Random.Range(i, enemysToSpawn.Count);
            enemysToSpawn[i] = enemysToSpawn[randomIndex];
            enemysToSpawn[randomIndex] = temp;
        }

        maxEnemysInScene = enemysToSpawn.Count;
        isActive = true;
    }

    void Update()
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
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
}
