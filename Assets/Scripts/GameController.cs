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
    [SerializeField] float enemySpeed = 1f;
    [SerializeField] float enemySpeedScaling = 1.05f;

    private int waveNumber = 0;
    private float currentHealth;
    private int liveEnemies;
    private float initialDifficulty;
    private float initialDensity;
    private float initialSpeed;

    private WaveSpawner waveSpawner;
    private WaveNumberDisplay waveNumberDisplay;
    private HealthDisplay healthDisplay;
    private LoseDisplay loseDisplay;
    private CannonShopDisplay cannonShopDisplay;
    private CannonShopController cannonShop;
    private CannonController cannonController;


    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        waveNumberDisplay = FindObjectOfType<WaveNumberDisplay>();
        healthDisplay = FindObjectOfType<HealthDisplay>();
        loseDisplay = FindObjectOfType<LoseDisplay>();
        cannonShopDisplay = FindObjectOfType<CannonShopDisplay>();
        cannonShop = FindObjectOfType<CannonShopController>();
        cannonController = FindObjectOfType<CannonController>();

        currentHealth = maxHealth;
        initialDensity = enemyDensity;
        initialDifficulty = enemyDifficulty;
        initialSpeed = enemySpeed;

        healthDisplay.SetMaxHealth(maxHealth);
        healthDisplay.DisplayHealth(currentHealth);
        SpawnWave();
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
        cannonController.DisableCannon();
    }

    public void SpawnWave()
    {
        waveNumber++;
        waveNumberDisplay.DisplayWave(waveNumber);
        waveSpawner.SpawnWaveWithDifficulty(enemyDifficulty, enemyDensity, enemySpeed);

        enemyDifficulty *= enemyDifficultyScaling;
        enemyDensity *= enemyDensityScaling;
        enemySpeed *= enemySpeedScaling;
        cannonController.EnableCannon();
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
        cannonController.DisableCannon();
    }

    public void RestartGame()
    {
        liveEnemies = 0;
        waveNumber = 0;
        currentHealth = maxHealth;
        enemyDifficulty = initialDifficulty;
        enemyDensity = initialDensity;
        enemySpeed = initialSpeed;
        healthDisplay.DisplayHealth(currentHealth);
        cannonController.EnableCannon();
        SpawnWave();
    }
}
