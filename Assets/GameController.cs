using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Things this needs to do:
    /// 
    /// - keep track of wave numer
    /// - initiate and spawn waves
    /// - inform UI of game state
    /// - check if enemies have gotten across finish line
    /// - keep track of player HP
    /// 
    /// later:
    /// - keep track of upgrades (??)
    /// </summary>

    private int waveNumber = 0;
    public float maxHealth = 10;
    private float currentHealth;
    public float enemyDifficulty = 10;
    public float enemyDifficultyScaling = 1.2f;
    public float enemyDensity = 0.1f;
    public float enemyDensityScaling = 1.05f;

    private int liveEnemies;

    private WaveSpawner waveSpawner;
    private NextWaveButton nextWaveButton;
    private WaveNumberDisplay waveNumberDisplay;
    private HealthDisplay healthDisplay;
    private LoseDisplay loseDisplay;

    private float initialDifficulty;
    private float initialDensity;

    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        nextWaveButton = FindObjectOfType<NextWaveButton>();
        waveNumberDisplay = FindObjectOfType<WaveNumberDisplay>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        loseDisplay = FindObjectOfType<LoseDisplay>();

        currentHealth = maxHealth;
        initialDensity = enemyDensity;
        initialDifficulty = enemyDifficulty;

        healthDisplay.SetMaxHealth(maxHealth);
        healthDisplay.DisplayHealth(currentHealth);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnWave();
        }
    }

    public void ReportDead()
    {
        liveEnemies--;
        if (liveEnemies <= 0)
        {
            WaveFinished();
        }
    }

    //Give upgrades and stuff here as well
    private void WaveFinished()
    {
        nextWaveButton.DisplayNextWaveButton(); 
    }

    public void SpawnWave()
    {
        waveNumber++;
        waveNumberDisplay.DisplayWave(waveNumber);
        waveSpawner.SpawnWaveWithDifficulty(enemyDifficulty, enemyDensity);

        enemyDifficulty *= enemyDifficultyScaling;
        enemyDensity *= enemyDensityScaling;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        healthDisplay.DisplayHealth(currentHealth);

        if (currentHealth <= 0)
        {
            EndGame();
        }
    }

    public void AddEnemies(int num)
    {
        liveEnemies += num;
    }

    private void EndGame()
    {
        loseDisplay.DisplayLose();
    }

    public void RestartGame()
    {
        liveEnemies = 0;
        waveNumber = 0;
        currentHealth = maxHealth;
        enemyDifficulty = initialDifficulty;
        enemyDensity = initialDensity;
        healthDisplay.DisplayHealth(currentHealth);
        SpawnWave();
    }
}
