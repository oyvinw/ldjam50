using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class GameController : MonoBehaviour
{

    [SerializeField] float enemyDifficulty = 10;
    [SerializeField] float enemyDifficultyScaling = 1.2f;
    [SerializeField] float enemyDensity = 0.1f;
    [SerializeField] float enemyDensityScaling = 1.05f;
    [SerializeField] float maxHealth = 10;

    private int waveNumber = 0;
    private float currentHealth;
    private int liveEnemies;
    private float initialDifficulty;
    private float initialDensity;

    private WaveSpawner waveSpawner;
    private WaveNumberDisplay waveNumberDisplay;
    private HealthDisplay healthDisplay;
    private LoseDisplay loseDisplay;
    private CannonShopDisplay cannonShopDisplay;
    private CannonShopController cannonShop;


    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        waveNumberDisplay = FindObjectOfType<WaveNumberDisplay>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        loseDisplay = FindObjectOfType<LoseDisplay>();
        cannonShopDisplay = FindObjectOfType<CannonShopDisplay>();
        cannonShop = FindObjectOfType<CannonShopController>();

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
        if (liveEnemies == 0 && waveSpawner.finishedSpawning)
        {
            WaveFinished();
        }
    }

    //Give upgrades and stuff here as well
    private void WaveFinished()
    {
        cannonShopDisplay.DisplayCannonShop();
        cannonShop.RestockShop();
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
