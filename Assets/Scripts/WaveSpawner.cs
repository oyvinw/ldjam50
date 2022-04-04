using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Transform> spawnPositions;
    public List<Enemy> enemyTypes;
    public float spawnRowInterval = 1f;
    public bool finishedSpawning;

    private List<Enemy> spawnedEnemies;

    private float currentDifficultyTotal;
    private GameController gc;


    private void Start()
    {
        spawnPositions = GetComponentsInChildren<Transform>().ToList();
        spawnPositions.RemoveAt(0);
        spawnedEnemies = new List<Enemy>();
        gc = FindObjectOfType<GameController>();
    }

    //Returns number of enemies spawned
    public void SpawnWaveWithDifficulty(float difficultyNum, float density, float speed)
    {
        ClearEnemies();
        finishedSpawning = false;
        currentDifficultyTotal = difficultyNum;
        var enemyTypesCount = enemyTypes.Count();

        StartCoroutine(SpawnWaves(difficultyNum, density, speed));
    }

    private IEnumerator SpawnWaves(float difficultyNum, float density, float speed)
    {
        var enemyCount = 0;
        var enemyTypesCount = enemyTypes.Count();
        var t = 0f;

        while (currentDifficultyTotal > 1)
        {
            if (t >= spawnRowInterval)
            {
                foreach (var spawnPos in spawnPositions)
                {
                    if (currentDifficultyTotal <= 1)
                        break;

                    if (Random.value < density)
                    {
                        var enemyType = enemyTypes[Random.Range(0, enemyTypesCount)];
                        var enemy = Instantiate(enemyType, spawnPos.position, transform.rotation, transform);
                        enemy.movementSpeed *= speed; 
                        spawnedEnemies.Add(enemy);
                        enemyCount++;
                        currentDifficultyTotal -= enemyType.spawnCost;
                    }
                }
                t = 0f;
                gc.AddEnemies(enemyCount);
                enemyCount = 0;
            }
            t += Time.deltaTime;
            yield return null;
        }
        finishedSpawning = true;
    }

    private void ClearEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                enemy.Destroy();
            }
        }
        spawnedEnemies = new List<Enemy>();
    }
}
